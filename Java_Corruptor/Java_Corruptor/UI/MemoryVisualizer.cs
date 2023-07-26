namespace MemoryVisualizerPlugin.UI
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using NLog;
    using RTCV.CorruptCore;
    using RTCV.NetCore;
    using RTCV.Common;
    using RTCV.UI;
    using MemoryVisualizerPlugin.Formats;
    using static RTCV.CorruptCore.RtcCore;
    using RTCV.Vanguard;
    using System.IO;

    public partial class MemoryVisualizer : Form
    {
        public volatile bool HideOnClose = true;
        private long pageSize = 256 * 256;

        private volatile int framesToNextExecute = 1;
        private volatile int delayFrames = 1;

        private object executeLock = new object();

        private long offset = 0;
        private long offsetIncr = 1;
        private long align = 0;

        //Offset fake volatile
        //object offsetLock = new object();
        //private long _offset = 0;
        //private long offset
        //{
        //    get { lock (offsetLock) { return _offset;  } }
        //    set { lock (offsetLock) { _offset = value; } }
        //}


        //object alignLock = new object();
        //private long _align = 0;
        //private long align
        //{
        //    get { lock (alignLock) { return _align; } }
        //    set { lock (alignLock) { _align = value; } }
        //}
        private volatile bool running = false;
        private volatile bool gettingBytes = false;
        //private volatile bool legacyLoop = false;

        private long rangeStartAddress = 0;
        private long rangeEndAddress = 0;
        //private MemoryInterface memoryInterface;

        private string Domain = null;

        Logger logger = NLog.LogManager.GetCurrentClassLogger();
        bool updatingDomains = false;


        public MemoryVisualizer()
        {
            this.InitializeComponent();
            this.cbDomains.SelectedIndexChanged += new EventHandler(this.CbDomains_SelectedIndexChanged);
            this.sliderOffset.ValueChanged += new Action<object, EventArgs>(this.SliderOffset_ValueChanged);
            this.sliderDelay.ValueChanged += new Action<object, EventArgs>(this.SliderDelay_ValueChanged);
            this.sliderOffset.ValueCorrection = new Func<long, long>(this.SliderOffsetCorrection);
            this.FormClosing += new FormClosingEventHandler(this.MemoryVisualizer_FormClosing);

            //Pixel formats
            string[] names = PixFormats.GetNames();
            this.display.curFormat = PixFormats.Get(names[0]);
            this.cbFormat.Items.AddRange(names);
            this.cbFormat.SelectedIndex = 0;
            this.offsetIncr = display.curFormat.BytesWide;
            this.cbFormat.SelectedIndexChanged += new EventHandler(this.CbFormat_SelectedIndexChanged);

            //Context menu
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(new MenuItem("Copy Image", (o, e2) => { lock (executeLock) { ImageClipboard.SetClipboardImage(display.bitmap.Bitmap, null, null); } })); //TODO: PNG
            contextMenu.MenuItems.Add(new MenuItem("Copy Range", (o, e2) => { lock (executeLock) { Clipboard.SetText(this.rangeStartAddress.ToString("X") + "-" + this.rangeEndAddress.ToString("X")); } }));
            this.display.ContextMenu = contextMenu;

            //refreshDel = Delegate.CreateDelegate(typeof(Action), display, "Refresh");
            //Set loop
            StepActions.StepEnd += this.StepActions_StepEnd;

            this.Load += MemoryVisualizer_Load;
        }

        private void MemoryVisualizer_Load(object sender, EventArgs e)
        {
            try
            {
                bRefreshDomains_Click(null, null);
            }
            catch
            {
                //No domains loaded
            }
        }

        private void SliderDelay_ValueChanged(object arg1, EventArgs arg2)
        {
            lock (executeLock)
            {
                this.delayFrames = (int)this.sliderDelay.Value;
                this.framesToNextExecute = this.delayFrames;
            }
        }

        //Gives illusion of smoothness, skipping frames where it is still getting the previous frame
        //executes on a task
        private void StepActions_StepEnd(object sender, EventArgs e)
        {
            if (running)
            {
                framesToNextExecute--;
                if (framesToNextExecute <= 0)
                {
                    if (!gettingBytes)
                    {
                        Task.Run(LoopMethod);
                    }
                    framesToNextExecute = delayFrames;
                }
            }
        }

        private void StartRunning()
        {
            this.running = true;
            this.framesToNextExecute = this.delayFrames;
            this.bLoop.Text = "Stop Updating";
        }

        private void StopRunning()
        {
            this.running = false;
            this.framesToNextExecute = this.delayFrames;
            this.bLoop.Text = "Start Updating";
        }

        private void CbDomains_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (updatingDomains) { return; }
            lock (executeLock)
            {
                this.Domain = cbDomains.SelectedItem?.ToString();
                UpdateAllSizes();
                UpdateImage();
            }
        }

        private void LoopMethod()
        {
            this.gettingBytes = true; //Outside the lock
            lock (this.executeLock)
            {
                try
                {
                    this.UpdateImage();
                }
                catch (OutOfMemoryException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    logger.Error("Failed to UpdateImage()");
                    this.StopRunning();
                    throw ex;
                }
            }
            this.gettingBytes = false; //Outside the lock
        }

        private void UpdateAllSizes()
        {
            var mi = MemoryDomains.GetInterface(Domain);
            if (mi == null) { return; }

            this.pageSize = (long)(display.W * display.H) * display.curFormat.BytesWide;
            this.offsetIncr = (long)this.display.curFormat.BytesWide;
            //if (this.domainSize <= 0L)
            //{
            //    return;
            //}
            var dSize = mi.Size;
            long max = dSize - this.pageSize - 1L - this.align % (long)this.display.curFormat.BytesWide;
            if (max < 0L)
            {
                return;
            }
            this.sliderOffset.Maximum = max;
            this.sliderOffset.Value = Math.Min(this.sliderOffset.Maximum, Math.Max(this.sliderOffset.Minimum, this.sliderOffset.Value - this.sliderOffset.Value % this.offsetIncr));
        }

        private void CbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            lock (executeLock)
            {
                //PixFormat curFormat = this.display.curFormat;
                this.display.curFormat = PixFormats.Get(this.cbFormat.SelectedItem.ToString());
                this.nAlignment.Maximum = this.display.curFormat.BytesWide - 1;

                if ((int)this.nWidth.Value % display.curFormat.Pixels > 0)
                {
                    this.nWidth.Value = this.nWidth.Value + ((int)this.nWidth.Value % display.curFormat.Pixels);
                }
                this.UpdateAllSizes();
                this.UpdateImage();
            }
        }

        private void MemoryVisualizer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.StopRunning();
            StepActions.StepEnd -= this.StepActions_StepEnd;
        }

        private long SliderOffsetCorrection(long val)
        {
            if (val % this.offsetIncr == 0L)
            {
                return val;
            }
            long curVal = val;
            if (curVal > this.offset && this.offsetIncr > 1L)
            {
                curVal += this.offsetIncr;
            }
            long correctedVal = curVal - curVal % this.offsetIncr;
            if (correctedVal > this.sliderOffset.Maximum)
                correctedVal = this.sliderOffset.Maximum;
            if (correctedVal < 0L)
                correctedVal = 0L;
            return correctedVal;
        }

        private void SliderOffset_ValueChanged(object sender, EventArgs e)
        {
            lock (executeLock)
            {
                this.offset = this.sliderOffset.Value;
                UpdateImage();
            }
        }

        private void UpdateImage()
        {
            //if (this.Domain == null) { return; } included in get interface
            var mi = MemoryDomains.GetInterface(Domain);
            if (mi == null) { return; }

            long start = this.offset + this.align;
            long end = start + ((long)(display.W * display.H) * this.display.curFormat.BytesWide);
            //long numBytesToGet = this.w * this.h * this.display.curFormat.BytesWide;
            if (end >= mi.Size)
            {
                end = mi.Size - 1;
            }

            byte[] byteArr = mi.PeekBytes(start, end, true);
            //byte[] byteArr = this.GetByteArr(start, start + (long)numBytesToGet);
            this.rangeStartAddress = start;
            this.rangeEndAddress = end + 1L; //+1 because vmds are exclusive
            if (byteArr == null)
            {
                this.rangeStartAddress = start;
                this.rangeEndAddress = end + 1L;
                return;
            }
            this.display.SetBytes(byteArr);
            this.display.Refresh();
        }

        //private byte[] GetByteArr(long start, long end)
        //{
        //    return end >= this.memoryInterface.Size ? (byte[])null : this.memoryInterface.PeekBytes(start, end, true);
        //}

        //TODO: test with citra
        private async Task LegacyLoop()
        {
            while (this.running)
            {
                try
                {
                    this.UpdateImage();
                }
                catch (OutOfMemoryException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    this.StopRunning();
                    throw ex;
                }
                await Task.Delay((int)this.sliderDelay.Value);
            }
        }

        private void UpdateImageSize()
        {
            lock (executeLock)
            {
                this.display.SetSize((int)this.nWidth.Value, (int)this.nHeight.Value);
                this.UpdateAllSizes();
                this.UpdateImage();
            }
        }

        int lastWidth = 256;
        private void nWidth_ValueChanged(object sender, EventArgs e)
        {

            if ((int)this.nWidth.Value % display.curFormat.Pixels > 0)
            {
                this.nWidth.Value = Math.Max(1, this.nWidth.Value + (((int)nWidth.Value > lastWidth ?  1 : -1) * ((int)this.nWidth.Value % display.curFormat.Pixels)));
            }
            else
            {
                lastWidth = (int)nWidth.Value;
                this.UpdateImageSize();
            }
            ////TODO: FIX
            //if (display.curFormat is PixYCbYCr)
            //{

            //}
            //else
            //{
            //    this.UpdateImageSize();
            //}
        }

        private void nHeight_ValueChanged(object sender, EventArgs e)
        {
            this.UpdateImageSize();
        }

        private void bPullOnce_Click(object sender, EventArgs e)
        {
            lock (executeLock)
            {
                UpdateImage();
            }
        }

        private async void bLoop_Click(object sender, EventArgs e)
        {
            lock (executeLock)
            {
                if (!this.running)
                {
                    this.bPullOnce.Enabled = false;
                    this.StartRunning();
                    //if (!this.legacyLoop)
                    //    return;
                    //await this.LegacyLoop();
                }
                else
                {
                    this.bPullOnce.Enabled = true;
                    this.StopRunning();
                }
            }
        }

        private void nAlignment_ValueChanged(object sender, EventArgs e)
        {
            lock (executeLock)
            {
                this.align = (long)this.nAlignment.Value;
                this.UpdateAllSizes();
                this.UpdateImage();
            }
        }
        private void bRefreshDomains_Click(object sender, EventArgs e)
        {
            this.StopRunning();
            lock (executeLock)
            {
                this.sliderOffset.Value = 0L;
                string previousDomain = this.Domain;
                this.Domain = null;

                this.updatingDomains = true; //Prevent updates
                this.cbDomains.Items.Clear();
                string[] strArray = null;
                try
                {
                    //Get memory domain names
                    strArray = (AllSpec.VanguardSpec[VSPEC.MEMORYDOMAINS_INTERFACES] as MemoryDomainProxy[])?.Select(x => x.Name)?.ToArray();
                    //strArray = (AllSpec.VanguardSpec[VSPEC.MEMORYDOMAINS_INTERFACES] as MemoryDomainProxy[]).Select(x => x.Name).ToArray();
                    if (strArray != null && strArray.Length > 0)
                    {
                        this.cbDomains.Items.AddRange(strArray);
                        this.updatingDomains = false;

                        if (previousDomain != null && this.cbDomains.Items.Contains(previousDomain))
                        {
                            this.Domain = previousDomain;
                            this.cbDomains.SelectedItem = previousDomain; // cbDomains.Items.Cast<string>().Where(x => x == previousDomain);//IDK
                            this.UpdateAllSizes();
                            this.UpdateImage();
                        }
                        else
                        {
                            this.cbDomains.SelectedIndex = 0;
                            this.Domain = cbDomains.SelectedItem.ToString(); //Unneeded?
                            this.UpdateAllSizes();
                            this.UpdateImage();
                        }
                    }
                    else
                    {
                        this.Domain = null;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    StopRunning();
                    this.Domain = null;
                    throw ex;
                }
            }
        }

        private async void bBackFull_Click(object sender, EventArgs e)
        {
            lock (executeLock)
            {
                this.sliderOffset.Value -= this.pageSize;
                this.UpdateImage();
            }
        }

        private async void bForwardPage_Click(object sender, EventArgs e)
        {
            lock (executeLock)
            {
                this.sliderOffset.Value += this.pageSize;
                this.UpdateImage();
                
            }
        }

        private void bMinusRow_Click(object sender, EventArgs e)
        {
            lock (executeLock)
            {
                this.sliderOffset.Value -= this.pageSize / (long)this.nHeight.Value * (long)this.nRowAmt.Value;
                this.UpdateImage();
            }
        }

        private void bPlusRow_Click(object sender, EventArgs e)
        {
            lock (executeLock)
            {
                this.sliderOffset.Value += this.pageSize / (long)this.nHeight.Value * (long)this.nRowAmt.Value;
                this.UpdateImage();
            }
        }

        private void cbLegacyLoop_CheckedChanged(object sender, EventArgs e)
        {
            //this.legacyLoop = this.cbLegacyLoop.Checked;
        }

        private void nRowAmt_ValueChanged(object sender, EventArgs e)
        {

        }

        private void bCopyRange_Click(object sender, EventArgs e)
        {
            lock (executeLock)
            {
                Clipboard.SetText(this.rangeStartAddress.ToString("X") + "-" + this.rangeEndAddress.ToString("X"));
            }
        }

        private void bCopyImage_Click(object sender, EventArgs e)
        {
            lock (executeLock)
            {
                //byte[] result = null;
                using (MemoryStream stream = new MemoryStream())
                {
                    //Bitmap b = new Bitmap(display.bitmap);
                    display.bitmap.Bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    if (stream.Length <= 0) { return; }
                    SaveFileDialog s = new SaveFileDialog();
                    s.Filter = "png files (*.png)|*.png";
                    var r = s.ShowDialog();
                    if (r == DialogResult.OK)
                    {
                        File.WriteAllBytes(s.FileName, stream.ToArray());
                    }
                    //using (MemoryStream ms = new MemoryStream(stream.ToArray()))
                    //{
                    //    IDataObject dataObj = new DataObject();
                    //    dataObj.SetData("PNG", false, stream);
                    //    Clipboard.SetDataObject(dataObj, true);
                    //    //Clipboard.SetData(DataFormats.Bitmap, Image.FromStream(ms));
                    //}
                    //result = stream.ToArray();
                }
            }
        }
    }
}
