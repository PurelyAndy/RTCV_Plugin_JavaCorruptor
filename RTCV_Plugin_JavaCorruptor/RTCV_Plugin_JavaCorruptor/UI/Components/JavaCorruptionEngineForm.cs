using Java_Corruptor.UI.Components.EngineControls;
using ObjectWeb.Asm.Tree;
using ObjectWeb.Asm;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using RTCV.UI.Modular;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RTCV.Common;
using Java_Corruptor.BlastClasses;
using Java_Corruptor.Javanguard;
using NLog;

namespace Java_Corruptor.UI.Components;

public partial class JavaCorruptionEngineForm : ComponentForm, IBlockable
{
    private void HandleMouseDown(object s, MouseEventArgs e) => this.HandleMouseDownP(s, e);
    private void HandleFormClosing(object s, FormClosingEventArgs e) => this.HandleFormClosingP(s, e);

    internal JavaEngineControl SelectedEngine;
    private ComboBox _comboBox;
    internal static SerializedInsnBlastLayerCollection BlastLayerCollection = new();
    internal static double Intensity;
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    // if only c# had a thread-safe hashset
    private static readonly ConcurrentDictionary<string, byte> SeenClasses = new();

    public JavaCorruptionEngineForm()
    {
        InitializeComponent();

        _comboBox = basicEngineControl1.placeholderComboBox;
        _comboBox.SelectedIndex = 0;
        SelectedEngine = basicEngineControl1;
        basicEngineControl1.Visible = true;

        basicEngineControl1.placeholderComboBox.SelectedIndexChanged += UpdateEngine;
        nukerEngineControl1.placeholderComboBox.SelectedIndexChanged += UpdateEngine;
        arithmeticEngineControl1.placeholderComboBox.SelectedIndexChanged += UpdateEngine;
        functionEngineControl1.placeholderComboBox.SelectedIndexChanged += UpdateEngine;
        javaCustomEngineControl1.placeholderComboBox.SelectedIndexChanged += UpdateEngine;
        stringEngineControl1.placeholderComboBox.SelectedIndexChanged += UpdateEngine;
        logicEngineControl1.placeholderComboBox.SelectedIndexChanged += UpdateEngine;
    }

    enum CorruptionResult
    {
        Unmodified, Modified, Canceled,
    }
    private CorruptionResult CorruptClass(byte[] fileBytes, out ClassWriter classWriter)
    {
        ClassReader classReader = new((sbyte[])(Array)fileBytes);
        ClassNode classNode = new();
        classWriter = new(CorruptionOptions.MethodCompute);

        classReader.Accept(classNode, 0);
        if (SeenClasses.ContainsKey(classNode.Name))
        {
            if (!JavaBlastTools.IgnoreDuplicateClasses && DialogResult.No == MessageBox.Show(
                    $"Duplicate class {classNode.Name} found in JAR. Consider deleting all files in the JAR's META-INF folder except MANIFEST.MF, or at least all .class files.\nWould you like to ignore this error and continue anyway?",
                    "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                return CorruptionResult.Canceled;
            SeenClasses.TryRemove(classNode.Name, out _);
            if (!JavaBlastTools.IgnoreDuplicateClasses)
                JavaBlastTools.IgnoreDuplicateClasses = true;
        }

        SeenClasses.TryAdd(classNode.Name, 0);
        bool modified = SelectedEngine.Corrupt(classNode);
        try
        {
            classNode.Accept(classWriter);
        }
        catch (ArgumentException e)
        {
            if (e.Message != "JSR/RET are not supported with computeFrames option")
                throw;
            
            classWriter = new(ClassWriter.Compute_Maxs);
            classNode.Accept(classWriter);
        }
        if (JavaBlastTools.IgnoreDuplicateClasses && !JavaBlastTools.IgnoreDuplicateClassesForever
            && DialogResult.Yes == MessageBox.Show("Would you like to always ignore duplicate class errors?","",
                MessageBoxButtons.YesNo))
        {
            JavaBlastTools.IgnoreDuplicateClassesForever = true;
        }
        else
        {
            JavaBlastTools.IgnoreDuplicateClasses = false;
        }
        return modified ? CorruptionResult.Modified : CorruptionResult.Unmodified;
    }
    
    private CorruptionResult CorruptClass(ClassNode classNode, out ClassWriter classWriter)
    {
        classWriter = new(CorruptionOptions.MethodCompute);
        if (SeenClasses.ContainsKey(classNode.Name))
        {
            if (!JavaBlastTools.IgnoreDuplicateClasses && DialogResult.No == MessageBox.Show(
                    $"Duplicate class {classNode.Name} found in JAR. Consider deleting all files in the JAR's META-INF folder except MANIFEST.MF, or at least all .class files.\nWould you like to ignore this error and continue anyway?",
                    "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                return CorruptionResult.Canceled;
            SeenClasses.TryRemove(classNode.Name, out _);
            if (!JavaBlastTools.IgnoreDuplicateClasses)
                JavaBlastTools.IgnoreDuplicateClasses = true;
        }
        SeenClasses.TryAdd(classNode.Name, 0);
        
        bool modified = SelectedEngine.Corrupt(classNode);
        try
        {
            classNode.Accept(classWriter);
        }
        catch (ArgumentException e)
        {
            if (e.Message != "JSR/RET are not supported with computeFrames option")
                throw;

            classWriter = new(ClassWriter.Compute_Maxs);
            classNode.Accept(classWriter);
        }
        if (JavaBlastTools.IgnoreDuplicateClasses && !JavaBlastTools.IgnoreDuplicateClassesForever
            && DialogResult.Yes == MessageBox.Show("Would you like to always ignore duplicate class errors?","",
                MessageBoxButtons.YesNo))
        {
            JavaBlastTools.IgnoreDuplicateClassesForever = true;
        }
        else
        {
            JavaBlastTools.IgnoreDuplicateClasses = false;
        }
        return modified ? CorruptionResult.Modified : CorruptionResult.Unmodified;
    }
    
    internal void Corrupt(bool useEngine = false)
    {
        JavaGeneralParametersForm gpForm = S.GET<JavaGeneralParametersForm>();
        gpForm.ResetRandom();
        
        SeenClasses.Clear();
        if (useEngine)
            BlastLayerCollection = new();

        Intensity = gpForm.Intensity;

        JavaEngineControl engine = SelectedEngine;
        if (SelectedEngine is BlastLayerApplierEngineControl && useEngine) // if an exception is thrown while applying a blast layer, the engine never gets reset
            UpdateEngine(_comboBox, EventArgs.Empty);
        if (!useEngine)
            SelectedEngine = new BlastLayerApplierEngineControl();

        SelectedEngine.Prepare();
        
        JavaBlastTools.ReloadClasses(true);

        if (CorruptModeInfo.Live)
        {
            CorruptLive(useEngine);
        }
        else
        {
            CorruptFileStub(useEngine);
        }
        
        gpForm.RunPostCorruptAction();
        
        if (!useEngine)
            SelectedEngine = engine;
    }

    private void CorruptFileStub(bool useEngine)
    {
        ConcurrentDictionary<string, sbyte[]> modifiedClasses = new();
        ConcurrentDictionary<string, byte[]> resources = new();

        Stopwatch stopwatch = new();
        string jarName = (string)AllSpec.VanguardSpec[VSPEC.OPENROMFILENAME];
        string outputFileName = Path.GetFileName(jarName) + "_corrupted_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".jar";
        string outputFilePath = Path.Combine(Path.GetDirectoryName(jarName)!, outputFileName);
        
        int threadCount = CorruptionOptions.Threads;
        stopwatch.Start();
        if (threadCount > 1)
        {
            try
            {
                Parallel.For(0, threadCount, DoCorrupt);
            }
            catch (AggregateException e)
            {
                new BetterCloudDebug(e).Start();
            }
        }
        else
            DoCorrupt(0);
        
        stopwatch.Stop();
        Logger.Info($"{threadCount} threads corrupted {SeenClasses.Count} classes in {stopwatch.ElapsedMilliseconds}ms");

        stopwatch.Restart();
        using (FileStream fileStream2 = File.OpenWrite(outputFilePath))
        using (ZipArchive zipArchive2 = new(fileStream2, ZipArchiveMode.Create))
        {
            foreach (KeyValuePair<string, sbyte[]> entry in modifiedClasses)
            {
                ZipArchiveEntry zipArchiveEntry = zipArchive2.CreateEntry(entry.Key,CorruptionOptions.CompressJar ? CompressionLevel.Fastest : CompressionLevel.NoCompression);
                using Stream stream = zipArchiveEntry.Open();
                stream.Write((byte[])(Array)entry.Value, 0, entry.Value.Length);
            }

            foreach (KeyValuePair<string, byte[]> entry in resources)
            {
                ZipArchiveEntry zipArchiveEntry = zipArchive2.CreateEntry(entry.Key, CorruptionOptions.CompressJar ? CompressionLevel.Fastest : CompressionLevel.NoCompression);
                using Stream stream = zipArchiveEntry.Open();
                stream.Write(entry.Value, 0, entry.Value.Length);
            }
        }
        stopwatch.Stop();
        Logger.Info($"Wrote corrupted jar in {stopwatch.ElapsedMilliseconds}ms");
        return;

        void DoCorrupt(int start)
        {
            Stopwatch sw = Stopwatch.StartNew();
            
            HashSet<string> keys = [];
            if (!useEngine)
                foreach (string entry in BlastLayerCollection.MappedLayers.Keys)
                    keys.Add(entry.Split('.')[0]);
            int i = 0;
            foreach ((string name, (ClassNode node, byte[] bytes, string path) c) in AsmUtilities.Classes)
            {
                if (i++ % threadCount != start)
                    continue;
                if (!useEngine)
                {
                    if (keys.Contains(name))
                    {
                        CorruptionResult result;
                        ClassWriter classWriter;
                        try
                        {
                            result = CorruptClass(c.node, out classWriter);
                        }
                        finally
                        {
                            ClassReader cr = new((sbyte[])(Array)c.bytes);
                            ClassNode clone = new();
                            cr.Accept(clone, 0);
                            AsmUtilities.Classes[name] = (clone, c.bytes, c.path);
                        }

                        if (result == CorruptionResult.Canceled)
                            return;
                        if (result == CorruptionResult.Modified)
                        {
                            ClassReader cr = new((sbyte[])(Array)c.bytes);
                            ClassNode clone = new();
                            cr.Accept(clone, 0);
                            AsmUtilities.Classes[name] = (clone, c.bytes, c.path);
                        }
                        modifiedClasses.TryAdd(c.path, classWriter.ToByteArray());
                    }
                    else
                        resources.TryAdd(c.path, c.bytes);
                }
                else
                {
                    CorruptionResult result;
                    ClassWriter classWriter;
                    if (CorruptionOptions.UseDomains && !CorruptionOptions.FilterClasses.Contains(c.node.Name))
                    {
                        result = CorruptionResult.Unmodified;
                        classWriter = null;
                    }
                    else
                    {
                        try
                        {
                            result = CorruptClass(c.node, out classWriter);
                        }
                        catch (Exception)
                        {
                            ClassReader cr = new((sbyte[])(Array)c.bytes);
                            ClassNode clone = new();
                            cr.Accept(clone, 0);
                            AsmUtilities.Classes[name] = (clone, c.bytes, c.path);
                            throw;
                        }
                    }
                    
                    if (result == CorruptionResult.Canceled)
                        return;
                    if (result == CorruptionResult.Unmodified)
                        resources.TryAdd(c.path, c.bytes);
                    else
                    {
                        ClassReader cr = new((sbyte[])(Array)c.bytes);
                        ClassNode clone = new();
                        cr.Accept(clone, 0);
                        AsmUtilities.Classes[name] = (clone, c.bytes, c.path);
                        modifiedClasses.TryAdd(c.path, classWriter!.ToByteArray());
                    }
                }
            }
            foreach (KeyValuePair<string, byte[]> kvp in AsmUtilities.NonClasses)
            {
                resources.TryAdd(kvp.Key, kvp.Value);
            }
            sw.Stop();
            Logger.Info($"Thread {start}: {sw.ElapsedMilliseconds}ms");
        }
    }

    private void CorruptLive(bool useEngine)
    {
        JavaConnector.SendResetOrder();
        Stopwatch stopwatch = new();
        ConcurrentDictionary<string, sbyte[]> modifiedClasses = new();
        
        int threadCount = CorruptionOptions.Threads;
        stopwatch.Start();
        Dictionary<string, byte[]>[] fuck = new Dictionary<string, byte[]>[threadCount];
        for (int i = 0; i < fuck.Length; i++)
        {
            fuck[i] = new();
        }
        int mod = 0;
        foreach (var kvp in CorruptModeInfo.ClassData)
        {
            fuck[mod++ % threadCount][kvp.Key] = kvp.Value;
        }

        if (threadCount > 1)
        {
            try
            {
                Parallel.For(0, threadCount, DoCorrupt);
            }
            catch (AggregateException e)
            {
                new BetterCloudDebug(e).Start();
            }
        }
        else
            DoCorrupt(0);
        
        stopwatch.Stop();
        Logger.Info($"Corrupted {AsmUtilities.Classes.Count} classes in {stopwatch.ElapsedMilliseconds}ms.");

        stopwatch.Restart();
        JavaConnector.SendMessage(new(modifiedClasses));
        stopwatch.Stop();
        Logger.Info($"Wrote corrupted jar in {stopwatch.ElapsedMilliseconds}ms");
        return;

        void DoCorrupt(int index)
        {
            HashSet<string> keys = [];
            if (!useEngine)
                foreach (string entry in BlastLayerCollection.MappedLayers.Keys)
                    keys.Add(entry.Split('.')[0]);
            
            foreach (KeyValuePair<string, byte[]> kvp in fuck[index])
            {
                if (!useEngine)
                {
                    if (keys.Contains(kvp.Key))
                    {
                        CorruptionResult result = CorruptClass(kvp.Value, out ClassWriter classWriter);
                        if (result == CorruptionResult.Canceled)
                            return;
                        if (result == CorruptionResult.Modified)
                            modifiedClasses.TryAdd(kvp.Key, classWriter.ToByteArray());
                    }
                }
                else
                {
                    CorruptionResult result = CorruptClass(kvp.Value, out ClassWriter classWriter);
                    if (result == CorruptionResult.Canceled)
                        return;
                    if (result == CorruptionResult.Modified)
                        modifiedClasses.TryAdd(kvp.Key, classWriter.ToByteArray());
                }
            }
        }
    }
    
    public void SetEngine(string engineName)
    {
        ComboBox box = SelectedEngine.placeholderComboBox;
        box.SelectedIndex = box.Items.IndexOf(engineName);
    }

    public void UpdateEngine(object sender, EventArgs e)
    {
        ComboBox box = (ComboBox)sender;
        if (box.SelectedIndex == -1)
        {
            Logger.Warn("Selected index is -1, returning");
            return;
        }

        basicEngineControl1.Visible = false;
        nukerEngineControl1.Visible = false;
        arithmeticEngineControl1.Visible = false;
        functionEngineControl1.Visible = false;
        javaCustomEngineControl1.Visible = false;
        stringEngineControl1.Visible = false;
        logicEngineControl1.Visible = false;

        string engineName = box.SelectedItem.ToString();

        switch (engineName)
        {
            case "Basic Engine":
                basicEngineControl1.Visible = true;
                SelectedEngine = basicEngineControl1;
                break;
            case "Arithmetic Engine":
                arithmeticEngineControl1.Visible = true;
                SelectedEngine = arithmeticEngineControl1;
                break;
            case "Function Engine":
                functionEngineControl1.Visible = true;
                SelectedEngine = functionEngineControl1;
                break;
            case "Custom Engine":
                javaCustomEngineControl1.Visible = true;
                SelectedEngine = javaCustomEngineControl1;
                break;
            case "String Engine":
                stringEngineControl1.Visible = true;
                SelectedEngine = stringEngineControl1;
                break;
            case "Logic Engine":
                logicEngineControl1.Visible = true;
                SelectedEngine = logicEngineControl1;
                break;
            case "Nuker Engine":
                nukerEngineControl1.Visible = true;
                SelectedEngine = nukerEngineControl1;
                break;
        }

        _comboBox = SelectedEngine.placeholderComboBox;
        _comboBox.Text = engineName;
        SelectedEngine.BringToFront();
    }

    internal string GetEngineName() => _comboBox.SelectedItem.ToString();
}

