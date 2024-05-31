using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using Java_Corruptor.UI;
using Newtonsoft.Json;
using ObjectWeb.Asm.Tree;
using ObjectWeb.Asm;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;

namespace Java_Corruptor.BlastClasses;

public class JavaBlastTools
{
    public static string LastBlastLayerSavePath { get; private set; }
    
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
            AsmUtilities.Classes.Clear();
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
            JsonHelper.Serialize(bl, fs, Formatting.Indented);

        LastBlastLayerSavePath = filename;
        return true;
    }

    public static void LoadClassesFromCurrentJar()
    {
        string jarName = (string)AllSpec.VanguardSpec[VSPEC.OPENROMFILENAME];
        using FileStream fileStream = File.OpenRead(jarName);
        using ZipArchive zipArchive = new(fileStream, ZipArchiveMode.Read);
        
        LoadClassesFromJar(zipArchive);
    }

    internal static void LoadClassesFromJar(ZipArchive zipArchive)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        int classCount = 0;
        foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
            if (zipArchiveEntry.FullName.EndsWith(".class"))
            {
                classCount++;
                using Stream stream = zipArchiveEntry.Open();
                byte[] classBytes = new byte[zipArchiveEntry.Length];
                int bytesRead = 0;

                do
                    bytesRead += stream.Read(classBytes, bytesRead, classBytes.Length - bytesRead);
                while (bytesRead < classBytes.Length);

                ClassReader classReader = new((sbyte[])(Array)classBytes);
                ClassNode classNode = new();
                classReader.Accept(classNode, 0);

                AsmUtilities.Classes.Add(classNode.Name, classNode);
            }
        stopwatch.Stop();
        NLog.LogManager.GetCurrentClassLogger().Info($"Loaded {classCount} classes in {stopwatch.ElapsedMilliseconds}ms");
    }
}