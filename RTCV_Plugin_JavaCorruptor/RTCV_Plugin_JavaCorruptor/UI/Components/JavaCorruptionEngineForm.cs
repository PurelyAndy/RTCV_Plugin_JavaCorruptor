using Java_Corruptor;
using Java_Corruptor.UI.Components.EngineControls;
using Newtonsoft.Json;
using ObjectWeb.Asm.Tree;
using ObjectWeb.Asm;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using RTCV.UI.Modular;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Java_Corruptor.UI.Components;
using RTCV.Common;
using RTCV.Common.CustomExtensions;
using Java_Corruptor.BlastClasses;
using NLog;

namespace Java_Corruptor.UI.Components;

public partial class JavaCorruptionEngineForm : ComponentForm, IBlockable
{
    private new void HandleMouseDown(object s, MouseEventArgs e) => typeof(ComponentForm).GetMethod("HandleMouseDown", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this,
        [s, e]);
    private new void HandleFormClosing(object s, FormClosingEventArgs e) => typeof(ComponentForm).GetMethod("HandleFormClosing", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this,
        [s, e]);

    internal static readonly Random Random = new();
    internal JavaEngineControl SelectedEngine;
    private ComboBox _comboBox;
    internal static SerializedInsnBlastLayerCollection BlastLayerCollection = new();
    internal static double Intensity;
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    public JavaCorruptionEngineForm()
    {
        InitializeComponent();

        _comboBox = javaVectorEngineControl1.placeholderComboBox;
        _comboBox.SelectedIndex = 0;
        SelectedEngine = javaVectorEngineControl1;
        javaVectorEngineControl1.Visible = true;

        javaVectorEngineControl1.placeholderComboBox.SelectedIndexChanged += UpdateEngine;
        nukerEngineControl1.placeholderComboBox.SelectedIndexChanged += UpdateEngine;
        arithmeticEngineControl1.placeholderComboBox.SelectedIndexChanged += UpdateEngine;
        functionEngineControl1.placeholderComboBox.SelectedIndexChanged += UpdateEngine;
        javaCustomEngineControl1.placeholderComboBox.SelectedIndexChanged += UpdateEngine;
        stringEngineControl1.placeholderComboBox.SelectedIndexChanged += UpdateEngine;
        roundingEngineControl1.placeholderComboBox.SelectedIndexChanged += UpdateEngine;
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

        javaVectorEngineControl1.Visible = false;
        nukerEngineControl1.Visible = false;
        arithmeticEngineControl1.Visible = false;
        functionEngineControl1.Visible = false;
        javaCustomEngineControl1.Visible = false;
        stringEngineControl1.Visible = false;
        roundingEngineControl1.Visible = false;

        string engineName = box.SelectedItem.ToString();

        switch (engineName)
        {
            case "Vector Engine":
                javaVectorEngineControl1.Visible = true;
                SelectedEngine = javaVectorEngineControl1;
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
            case "Rounding Engine":
                roundingEngineControl1.Visible = true;
                SelectedEngine = roundingEngineControl1;
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

    internal void Corrupt(bool useEngine = false)
    {
        Stopwatch stopwatch = new();
        JavaGeneralParametersForm gpForm = S.GET<JavaGeneralParametersForm>();
        gpForm.ResetRandom();
        if (useEngine)
            BlastLayerCollection = new();

        string jarName = (string)AllSpec.VanguardSpec[VSPEC.OPENROMFILENAME];
        string outputFileName = Path.GetFileName(jarName) + "_corrupted_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".jar";
        string outputFilePath = Path.Combine(Path.GetDirectoryName(jarName)!, outputFileName);
        string inputFilePath = jarName;
        
        Intensity = gpForm.Intensity;

        JavaEngineControl engine = SelectedEngine;
        if (!useEngine)
            SelectedEngine = new BlastLayerApplierEngineControl();

        Dictionary<string, sbyte[]> modifiedClasses = new();
        Dictionary<string, byte[]> resources = new();
        AsmUtilities.Classes.Clear();
        
        using (FileStream fileStream = File.OpenRead(inputFilePath))
        using (ZipArchive zipArchive = new(fileStream, ZipArchiveMode.Read))
        {
            if (!useEngine)
                JavaBlastTools.LoadClassesFromJar(zipArchive);

            SelectedEngine.Prepare();

            if (!useEngine)
            {
                stopwatch.Start();
                int i = 0;
                foreach (ClassNode classNode in AsmUtilities.Classes.Values)
                {
                    SelectedEngine.Corrupt(classNode);

                    ClassWriter classWriter = new(ClassWriter.Compute_Maxs);
                    
                    classNode.Accept(classWriter);
                    
                    while (!zipArchive.Entries[i].FullName.EndsWith(".class"))
                    {
                        i++;
                    }
                    modifiedClasses.Add(zipArchive.Entries[i++].FullName, classWriter.ToByteArray());
                }
                stopwatch.Stop();
                Logger.Info($"Corrupted {AsmUtilities.Classes.Count} classes in {stopwatch.ElapsedMilliseconds}ms");
                
                stopwatch.Restart();
                foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
                {
                    if (zipArchiveEntry.FullName.EndsWith(".class"))
                        continue;

                    using Stream stream = zipArchiveEntry.Open();

                    byte[] fileBytes = new byte[zipArchiveEntry.Length];
                    int bytesRead = 0;

                    do
                        bytesRead += stream.Read(fileBytes, bytesRead, fileBytes.Length - bytesRead);
                    while (bytesRead < fileBytes.Length);

                    resources.Add(zipArchiveEntry.FullName, fileBytes);
                }
                stopwatch.Stop();
                Logger.Info($"Loaded {resources.Count} resources in {stopwatch.ElapsedMilliseconds}ms");
            }
            else
            {
                stopwatch.Start();
                foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
                {
                    using Stream stream = zipArchiveEntry.Open();

                    byte[] fileBytes = new byte[zipArchiveEntry.Length];
                    int bytesRead = 0;

                    do
                        bytesRead += stream.Read(fileBytes, bytesRead, fileBytes.Length - bytesRead);
                    while (bytesRead < fileBytes.Length);

                    if (zipArchiveEntry.FullName.EndsWith(".class"))
                    {
                        ClassReader classReader = new((sbyte[])(Array)fileBytes);
                        ClassNode classNode = new();

                        classReader.Accept(classNode, 0);

                        AsmUtilities.Classes.Add(classNode.Name, classNode);

                        if (zipArchiveEntry.FullName.Contains("goq.class"))
                        {
                            Logger.Info("Corrupted goq.class");
                        }

                        SelectedEngine.Corrupt(classNode);
                        
                        ClassWriter classWriter = new(ClassWriter.Compute_Maxs);
                        classNode.Accept(classWriter);
                        modifiedClasses.Add(zipArchiveEntry.FullName, classWriter.ToByteArray());
                    }
                    else
                        resources.Add(zipArchiveEntry.FullName, fileBytes);
                }
                stopwatch.Stop();
                Logger.Info($"Corrupted {AsmUtilities.Classes.Count} classes in {stopwatch.ElapsedMilliseconds}ms");
            }

            stopwatch.Restart();
            using (FileStream fileStream2 = File.OpenWrite(outputFilePath))
            using (ZipArchive zipArchive2 = new(fileStream2, ZipArchiveMode.Create))
            {
                foreach (var entry in modifiedClasses)
                {
                    ZipArchiveEntry zipArchiveEntry = zipArchive2.CreateEntry(entry.Key);
                    using Stream stream = zipArchiveEntry.Open();
                    stream.Write((byte[])(Array)entry.Value, 0, entry.Value.Length);
                }

                foreach (var entry in resources)
                {
                    ZipArchiveEntry zipArchiveEntry = zipArchive2.CreateEntry(entry.Key);
                    using Stream stream = zipArchiveEntry.Open();
                    stream.Write(entry.Value, 0, entry.Value.Length);
                }
            }
            stopwatch.Stop();
            Logger.Info($"Wrote corrupted jar in {stopwatch.ElapsedMilliseconds}ms");

            /* We don't need to save a blast layer separately anymore, blasts go to the stash history
            if (!useEngine) // argument for this? we might want to save the blast layer if we're merging blast layers or something
                return;

            string json = JsonConvert.SerializeObject(BlastLayerCollection, Formatting.Indented);
            File.WriteAllText(outputFilePath + ".jbl", json);
            */

            gpForm.RunPostCorruptAction();
        }
        
        if (!useEngine)
            SelectedEngine = engine;
    }
}

