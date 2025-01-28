using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using Java_Corruptor.Javanguard;
using ObjectWeb.Asm.Tree;
using ObjectWeb.Asm;
using RTCV.CorruptCore;
using RTCV.NetCore;

namespace Java_Corruptor.BlastClasses;

public static class JavaBlastTools
{
    public static string LastJarFilePath;
    public static long LastJarFileSize;
    public static bool NonClassesLoaded;
    public static string LastBlastLayerSavePath { get; private set; }
    internal static bool IgnoreDuplicateClasses;
    internal static bool IgnoreDuplicateClassesForever;
    
    public static SerializedInsnBlastLayerCollection LoadBlastLayerFromFile(string path = null)
    {
        if (path == null)
        {
            OpenFileDialog ofd = new()
            {
                DefaultExt = "jbl",
                Title = "Open Java BlastLayer File",
                Filter = "jbl files|*.jbl",
                RestoreDirectory = true,
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;
            }
            else
            {
                return null;
            }
        }

        if (!File.Exists(path))
        {
            MessageBox.Show($"The BlastLayer file {path} wasn't found");
            return null;
        }

        try
        {
            using FileStream fs = new(path, FileMode.Open);
            SerializedInsnBlastLayerCollection bl = JsonHelper.Deserialize<SerializedInsnBlastLayerCollection>(fs);
            
            return bl;
        }
        catch
        {
            MessageBox.Show($"The BlastLayer file {path} could not be loaded");
            return null;
        }
    }
    
    public static bool SaveBlastLayerToFile(SerializedInsnBlastLayerCollection bl, string path = null)
    {
        if (bl == null)
        {
            throw new ArgumentNullException(nameof(bl));
        }

        string filename = path;

        if (bl.MappedLayers.Count == 0)
        {
            MessageBox.Show("Can't save because the provided BlastLayer is empty is");
            return false;
        }

        if (filename == null)
        {
            SaveFileDialog saveFileDialog1 = new()
            {
                DefaultExt = "bl",
                Title = "Save BlastLayer File",
                Filter = "jbl files|*.jbl",
                RestoreDirectory = true,
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = saveFileDialog1.FileName;
            }
            else
            {
                return false;
            }
        }

        using (FileStream fs = new(filename, FileMode.Create))
            JsonHelper.Serialize(bl, fs);

        LastBlastLayerSavePath = filename;
        return true;
    }

    public static void ReloadClasses(bool nonClassesToo = false)
    {
        if (CorruptModeInfo.Live)
            LoadClassesFromClassData();
        else
            LoadClassesFromJar((string)AllSpec.VanguardSpec[VSPEC.OPENROMFILENAME], nonClassesToo);
    }

    internal static void LoadClassesFromJar(string jarName, bool nonClassesToo = false)
    {
        bool willSkipClasses = jarName == LastJarFilePath && new FileInfo(jarName).Length == LastJarFileSize;
        if (willSkipClasses)
        {
            NLog.LogManager.GetCurrentClassLogger().Info("Jar file already loaded, saved time by skipping loading classes");
            if (nonClassesToo && !NonClassesLoaded)
            {
                NLog.LogManager.GetCurrentClassLogger().Info("...but we still need to load non-classes");
            }
            else
            {
                return;
            }
        }
        else
        {
            LastJarFilePath = jarName;
            LastJarFileSize = new FileInfo(jarName).Length;
        }
        NonClassesLoaded = nonClassesToo;
        
        if (nonClassesToo)
        {
            AsmUtilities.NonClasses.Clear();
        }
        if (!willSkipClasses) {
            AsmUtilities.Classes.Clear();
        }
        using FileStream fileStream = File.OpenRead(jarName);
        using ZipArchive zipArchive = new(fileStream, ZipArchiveMode.Read);
        Stopwatch stopwatch = Stopwatch.StartNew();
        int classCount = 0;
        foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
        {
            if (!willSkipClasses && zipArchiveEntry.FullName.EndsWith(".class"))
            {
                classCount++;
                using Stream stream = zipArchiveEntry.Open();
                byte[] fileBytes = new byte[zipArchiveEntry.Length];
                int bytesRead = 0;

                do
                    bytesRead += stream.Read(fileBytes, bytesRead, fileBytes.Length - bytesRead);
                while (bytesRead < fileBytes.Length);

                ClassReader classReader = new((sbyte[])(Array)fileBytes);
                ClassNode classNode = new();
                classReader.Accept(classNode, 0);

                if (AsmUtilities.Classes.ContainsKey(classNode.Name))
                {
                    stopwatch.Stop();
                    if (!IgnoreDuplicateClasses && DialogResult.No == MessageBox.Show(
                            $"Duplicate class {classNode.Name} found in JAR. Consider deleting all files in the JAR's META-INF folder except MANIFEST.MF.\nWould you like to ignore this error and continue anyway?",
                            "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                        return;
                    stopwatch.Start();
                    AsmUtilities.Classes.TryRemove(classNode.Name, out _);
                    if (!IgnoreDuplicateClasses)
                        IgnoreDuplicateClasses = true;
                }

                AsmUtilities.Classes.TryAdd(classNode.Name, (classNode, fileBytes));
            }
            else if (nonClassesToo && !zipArchiveEntry.FullName.EndsWith(".class"))
            {
                using Stream stream = zipArchiveEntry.Open();
                byte[] nonClassBytes = new byte[zipArchiveEntry.Length];
                int bytesRead = 0;

                do
                    bytesRead += stream.Read(nonClassBytes, bytesRead, nonClassBytes.Length - bytesRead);
                while (bytesRead < nonClassBytes.Length);

                AsmUtilities.NonClasses.TryAdd(zipArchiveEntry.FullName, nonClassBytes);
            }
        }
        stopwatch.Stop();
        NLog.LogManager.GetCurrentClassLogger().Info($"Loaded {classCount} classes in {stopwatch.ElapsedMilliseconds}ms");
        if (IgnoreDuplicateClasses && !IgnoreDuplicateClassesForever
            && DialogResult.Yes == MessageBox.Show("Would you like to always ignore duplicate class errors?","",
                MessageBoxButtons.YesNo))
        {
            IgnoreDuplicateClassesForever = true;
        }
        else
        {
            IgnoreDuplicateClasses = false;
        }
    }

    internal static void LoadClassesFromClassData()
    {
        AsmUtilities.Classes.Clear();
        Stopwatch stopwatch = Stopwatch.StartNew();
        int classCount = 0;
        foreach (byte[] classBytes in CorruptModeInfo.ClassData.Values)
        {
            ClassReader classReader = new((sbyte[])(Array)classBytes);
            ClassNode classNode = new();
            classReader.Accept(classNode, 0);

            if (AsmUtilities.Classes.ContainsKey(classNode.Name))
            {
                stopwatch.Stop();
                if (!IgnoreDuplicateClasses && DialogResult.No == MessageBox.Show($"Duplicate class {classNode.Name} found in JAR. Consider deleting all files in the JAR's META-INF folder except MANIFEST.MF.\nWould you like to ignore this error and continue anyway?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                    return;
                stopwatch.Start();
                AsmUtilities.Classes.TryRemove(classNode.Name, out _);
                if (!IgnoreDuplicateClasses)
                    IgnoreDuplicateClasses = true;
            }
            AsmUtilities.Classes.TryAdd(classNode.Name, (classNode, classBytes));
        }
        stopwatch.Stop();
        NLog.LogManager.GetCurrentClassLogger().Info($"Loaded {classCount} classes in {stopwatch.ElapsedMilliseconds}ms");
        if (IgnoreDuplicateClasses && !IgnoreDuplicateClassesForever
            && DialogResult.Yes == MessageBox.Show("Would you like to always ignore duplicate class errors?","",
                MessageBoxButtons.YesNo))
        {
            IgnoreDuplicateClassesForever = true;
        }
        else
        {
            IgnoreDuplicateClasses = false;
        }
    }
}