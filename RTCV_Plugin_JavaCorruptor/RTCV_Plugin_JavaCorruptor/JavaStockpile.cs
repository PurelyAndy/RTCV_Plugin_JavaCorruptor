using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using Java_Corruptor.BlastClasses;
using Java_Corruptor.UI;
using Newtonsoft.Json;
using RTCV.Common;
using RTCV.Common.Objects;
using RTCV.CorruptCore;
using RTCV.CorruptCore.Exceptions;
using RTCV.NetCore;

namespace Java_Corruptor;

internal class JavaStockpile
{
    public List<JavaStashKey> StashKeys { get; } = new();
    public string Filename { get; private set; }
        
    public JavaStockpile(DataGridView dgvStockpile)
    {
        if (dgvStockpile == null)
        {
            throw new ArgumentNullException(nameof(dgvStockpile));
        }

        foreach (DataGridViewRow row in dgvStockpile.Rows)
        {
            StashKeys.Add((JavaStashKey)row.Cells[0].Value);
        }
    }

    public JavaStockpile() { }
        
    public static bool Save(JavaStockpile sks, string filename, bool includeReferencedFiles = false, bool compress = true)
    {
        if (sks == null)
        {
            throw new ArgumentNullException(nameof(sks));
        }

        try
        {
            if (sks.StashKeys.Count == 0)
            {
                MessageBox.Show("Can't save because the Current Stockpile is empty");
                throw new StockpileSaveException("Can't save because the Current Stockpile is empty");
            }

            sks.Filename = filename;

            decimal saveProgress = 0;
            CleanTempFolder(ref sks, ref saveProgress);

            CopyReferencedFiles(sks, includeReferencedFiles, ref saveProgress);

            CreateStockpileJson(ref sks, ref saveProgress);

            CreateAndReplaceStockpileZip(ref sks, compress, ref saveProgress);

            CleanOutStockpileFolder(ref sks, ref saveProgress);

            RtcCore.OnProgressBarUpdate(sks, new("Done", saveProgress = 100));
        }
        catch (StockpileSaveException)
        {
            return false;
        }

        return true;
    }

    private static void CleanTempFolder(ref JavaStockpile sks, ref decimal saveProgress)
    {
        try
        {
            RtcCore.OnProgressBarUpdate(sks, new("Emptying TEMP", saveProgress += 2));
            EmptyFolder(Path.Combine("WORKING", "TEMP"));
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            throw new StockpileSaveException(e.Message);
        }
    }

    private static void CopyReferencedFiles(JavaStockpile sks, bool includeReferencedFiles, ref decimal saveProgress)
    {
        List<string> allRoms = new();
        if (includeReferencedFiles)
        {
            RtcCore.OnProgressBarUpdate(sks, new("Prepping referenced files", saveProgress += 2));
            //populating Allroms array
            foreach (JavaStashKey key in sks.StashKeys)
            {
                if (allRoms.Contains(key.JarFilename))
                    continue;
                    
                allRoms.Add(key.JarFilename);
            }

            decimal percentPerFile = 20m / (allRoms.Count + 1);
            //populating temp folder with roms
            foreach (string str in allRoms)
            {
                if (str.EndsWith("IGNORE"))
                    continue;

                RtcCore.OnProgressBarUpdate(sks, new($"Copying {Path.GetFileNameWithoutExtension(str)} to stockpile", saveProgress += percentPerFile));
                string rom = str;
                string romTempfilename = Path.Combine(RtcCore.workingDir, "TEMP", Path.GetFileName(rom));

                if (!File.Exists(rom))
                {
                    if (MessageBox.Show($"Include referenced files was set but we couldn't find {rom}. Continue saving? (You'll need to reassociate the file at runtime)", "Couldn't find file.", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        throw new StockpileSaveException("Included referenced files weren't found");
                    }
                }
                else
                {
                    //If the file already exists, overwrite it.
                    if (File.Exists(romTempfilename))
                    {
                        //Whack the attributes in case a rom is readonly
                        File.SetAttributes(romTempfilename, FileAttributes.Normal);
                        File.Delete(romTempfilename);
                        File.Copy(rom, romTempfilename);
                    }
                    else
                    {
                        File.Copy(rom, romTempfilename);
                    }
                }
            }

            //Update the paths
            RtcCore.OnProgressBarUpdate(sks, new("Fixing paths", saveProgress += 2));
            foreach (JavaStashKey sk in sks.StashKeys)
            {
                sk.JarShortFilename = Path.GetFileName(sk.JarFilename);
                sk.JarFilename = Path.Combine(RtcCore.workingDir, "SKS", sk.JarShortFilename);
            }
        }
        else
        {
            bool failure = false;
            //Gotta do this on the UI thread.
            SyncObjectSingleton.FormExecute(() =>
            {
                // We need to handle if they aren't including referenced files but the file is within the working dir where they'll get deleted (temp, sks, etc)
                foreach (JavaStashKey key in sks.StashKeys)
                {
                    string message = $"Can't save with file {key.JarFilename}\nGame name: {key.GameName}\n\nThis file appears to be in temporary storage (e.g. loaded from a stockpile).\nTo save without references, you will need to provide a replacement from outside the RTC's working directory.\n\nPlease provide a new path to the file in question.";
                    while (!string.IsNullOrEmpty(key.JarFilename) && CorruptCoreExtensions.IsOrIsSubDirectoryOf(Path.GetDirectoryName(key.JarFilename), RtcCore.workingDir)) // Make sure they don't give a new file within working
                    {
                        if (JavaStockpileManagerUISide.CheckAndFixMissingReference(key, true, sks.StashKeys, "Reference found in RTC dir", message))
                            continue;
                        failure = true;
                        return;
                    }
                }
            });

            if (failure)
            {
                throw new StockpileSaveException("Missing file reference");
            }
        }
    }

    private static void CreateStockpileJson(ref JavaStockpile sks, ref decimal saveProgress)
    {
        //Create stockpile.json to temp folder from stockpile object
        using FileStream fs = File.Open(Path.Combine(RtcCore.workingDir, "TEMP", "stockpile.json"), FileMode.OpenOrCreate);
        RtcCore.OnProgressBarUpdate(sks, new("Creating stockpile.json", saveProgress += 2));

        AsmUtilities.Classes.Clear();
        JavaBlastTools.LoadClassesFromCurrentJar();
                        
        JsonHelper.Serialize(sks, fs, Formatting.Indented);
        AsmUtilities.Classes.Clear();
    }

    private static void CreateAndReplaceStockpileZip(ref JavaStockpile sks, bool compress, ref decimal saveProgress)
    {
        string tempFilename = sks.Filename + ".temp";
        //If there's already a temp file from a previous failed save, delete it
        try
        {
            if (File.Exists(tempFilename))
                File.Delete(tempFilename);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            throw (StockpileSaveException)ex;
        }

        CompressionLevel comp = compress ? CompressionLevel.Fastest : CompressionLevel.NoCompression;

        RtcCore.OnProgressBarUpdate(sks, new("Creating SKS", saveProgress += 10));
        //Create the file into temp
        ZipFile.CreateFromDirectory(Path.Combine(RtcCore.workingDir, "TEMP"), tempFilename, comp, false);

        //Remove the old stockpile
        try
        {
            RtcCore.OnProgressBarUpdate(sks, new("Removing old stockpile", saveProgress += 2));
            if (File.Exists(sks.Filename))
                File.Delete(sks.Filename);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            throw (StockpileSaveException)ex;
        }

        //Move us to the destination
        RtcCore.OnProgressBarUpdate(sks, new("Moving SKS to destination", saveProgress += 2));
        File.Move(tempFilename, sks.Filename);
    }

    private static void CleanOutStockpileFolder(ref JavaStockpile sks, ref decimal saveProgress)
    {
        try
        {
            RtcCore.OnProgressBarUpdate(sks, new("Emptying SKS", saveProgress += 2));
            EmptyFolder(Path.Combine("WORKING", "SKS"));
        }
        catch (Exception e)
        {
            Console.Write(e);
            MessageBox.Show("Unable to empty the stockpile folder. There's probably something locking a file inside it (iso based game loaded?)\n. Your stockpile is saved, but your current session is bunk.\nRe-load the file");
        }
    }

    public static OperationResults<JavaStockpile> Load(string filename, bool import = false)
    {
        OperationResults<JavaStockpile> results = new();
        JavaStockpile sks;

        decimal loadProgress = 0;

        if (!File.Exists(filename))
        {
            results.AddError("The selected stockpile was not found.");
            return results;
        }

        string extractFolder = import ? "TEMP" : "SKS";

        //Extract the stockpile
        RtcCore.OnProgressBarUpdate(null, new("Extracting Stockpile (progress not reported during extraction)", loadProgress += 5));
        OperationResults<bool> extractionResults = Extract(filename, Path.Combine("WORKING", extractFolder), "stockpile.json");
        if (extractionResults?.Failed == true)
        {
            results.AddResults(extractionResults);
            return results;
        }

        //Read in the stockpile
        try
        {
            RtcCore.OnProgressBarUpdate(null, new("Reading Stockpile", loadProgress += 45));
            using FileStream fs = File.Open(Path.Combine(RtcCore.workingDir, extractFolder, "stockpile.json"), FileMode.OpenOrCreate);
            sks = JsonHelper.Deserialize<JavaStockpile>(fs);
        }
        catch (Exception e)
        {
            results.AddError("Failed to read the stockpile", e);
            return results;
        }

        if (import)
        {
            List<string> allCopied = new();
            //Copy from temp to sks
            string[] files = Directory.GetFiles(Path.Combine(RtcCore.workingDir, "TEMP"));
            decimal percentPerFile = 20m / (files.Length + 1);
            foreach (string file in files)
            {
                RtcCore.OnProgressBarUpdate(sks, new($"Merging {Path.GetFileNameWithoutExtension(file)} to stockpile", loadProgress += percentPerFile));
                if (file.Contains(".sk"))
                    continue;
                try
                {
                    string dest = Path.Combine(RtcCore.workingDir, "SKS", Path.GetFileName(file));

                    //Only copy if a version doesn't exist
                    //This prevents copying over keys
                    if (!File.Exists(dest))
                    {
                        File.Copy(file, dest); // copy roms/stockpile/whatever to sks folder
                        allCopied.Add(dest);
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        foreach (string f in allCopied)
                        {
                            File.Delete(f);
                        }
                    }
                    catch
                    {
                        // ignored
                    }

                    results.AddError("Unable to copy a file from temp to sks. The culprit is " + file + ".\nCancelling operation.\n ", ex);
                    return results;
                }
            }
            EmptyFolder(Path.Combine("WORKING", "TEMP"));
        }
        else
        {
            //Update the filename in case they renamed it
            sks.Filename = filename;
        }

        RtcCore.OnProgressBarUpdate(sks, new("Done", 100));

        results.Result = sks;

        return results;
    }

    public static OperationResults<JavaStockpile> Import(string filename)
    {
        return Load(filename, true);
    }

    /// <summary>
    /// Recursively deletes all files and folders within a directory
    /// </summary>
    /// <param name="baseDir"></param>
    private static void RecursiveDelete(DirectoryInfo baseDir)
    {
        if (!baseDir.Exists)
        {
            return;
        }

        foreach (DirectoryInfo dir in baseDir.EnumerateDirectories())
        {
            RecursiveDelete(dir);
        }
        baseDir.Delete(true);
    }

    public static void EmptyFolder(string folder)
    {
        try
        {
            string targetFolder = Path.Combine(RtcCore.RtcDir, folder);

            foreach (string file in Directory.GetFiles(targetFolder))
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in Directory.GetDirectories(targetFolder))
            {
                RecursiveDelete(new(dir));
            }
        }
        catch (Exception ex)
        {
            throw new("Unable to empty a temp folder! If your stockpile has any CD based games, close them before saving the stockpile! If this isn't the case, report this bug to the RTC developers." + ex.Message);
        }
    }

    /// <summary>
    /// Extracts a stockpile into a folder and ensures a master file exists
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="folder"></param>
    /// <param name="masterFile"></param>
    /// <returns></returns>
    public static OperationResults<bool> Extract(string filename, string folder, string masterFile)
    {
        OperationResults<bool> r = new();
        try
        {
            EmptyFolder(folder);
            ZipFile.ExtractToDirectory(filename, Path.Combine(RtcCore.RtcDir, folder));

            if (File.Exists(Path.Combine(RtcCore.RtcDir, folder, masterFile)))
                return r;
                
            r.AddError("The file could not be read properly. Master file missing");

            EmptyFolder(folder);
            return r;
        }
        catch (Exception e)
        {
            //If it errors out, empty the folder
            EmptyFolder(folder);
            r.AddError("The file could not be read properly (an error occurred, check the log file for more details)", e);
            return r;
        }
    }
}