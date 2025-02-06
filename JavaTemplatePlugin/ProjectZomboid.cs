using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using FileStub;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using RTCV.CorruptCore;

namespace JavaTemplatePlugin
{
    public partial class ProjectZomboid : Form, IFileStubTemplate
    {
        private string _jarPath;
        public ProjectZomboid()
        {
            InitializeComponent();
        }

        public string[] TemplateNames => ["Project Zomboid : Java Class Files"];

        public bool DisplayBrowseTarget => true;

        public bool DisplayDragAndDrop => true;

        public void BrowseFiles()
        {
            OpenFileDialog ofd = new()
            {
                Title = "Locate ProjectZomboid64.bat",
                Filter = "ProjectZomboid64.bat|ProjectZomboid64.bat",
                Multiselect = false,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            SelectFile(ofd.FileName);
        }

        public void GetSegments(FileInterface exeInterface)
        {
        }

        public FileTarget[] GetTargets()
        {
            // stupid hack. if i don't do this, the singular jar will be imported with a MultipleFileInterface,
            // so VSPEC.OPENROMFILENAME won't have the location of the rom
            FileWatch.currentSession.selectedTargetType = TargetType.SINGLE_FILE;
            return [ new(Path.GetFileName(_jarPath), Path.GetDirectoryName(_jarPath) + '\\') ];
        }

        public Form GetTemplateForm(string templateName)
        {
            return this;
        }

        bool IFileStubTemplate.DragDrop(string[] fd)
        {
            if (fd.Length != 1 || Path.GetFileName(fd[0]) != "ProjectZomboid64.bat")
            {
                MessageBox.Show("Please drop only one file, the ProjectZomboid64.bat from your installation of Project Zomboid.", "Wrong file", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return false;
            }

            SelectFile(fd[0]);
            return true;
        }

        private void SelectFile(string pzBat)
        {
            string classesFolder = Path.GetDirectoryName(pzBat) + @"\zombie\";
            string zombieJar = classesFolder + "zombie.jar";
            string pzBatRtc = $"{Path.GetDirectoryName(pzBat)}\\ProjectZomboid64_rtc.bat";
            if (Directory.GetFiles(classesFolder).Any(f => f.EndsWith(".jls")))
            {
                // Reset so that we can do it all over again just in case we did it wrong the last time, you know?
                try
                {
                    using FileStream stream = File.OpenRead(classesFolder + "zombie_corrupt.jar");
                    using ZipArchive backup = new(stream);
                    foreach (ZipArchiveEntry entry in backup.Entries)
                    {
                        string fullName = Path.GetFullPath(Path.Combine(classesFolder, entry.Name));
                        if (entry.Name == "")
                        {
                            Directory.CreateDirectory(fullName);
                            continue;
                        }
                        entry.ExtractToFile(fullName, true);
                    }
                }
                catch { }
                string[] toDelete = [zombieJar, classesFolder + "zombie_backup.jar", classesFolder + "zombie_corrupt.jar", classesFolder + "zomboid.bat", classesFolder + "zomboid.jls", pzBatRtc];
                foreach (var path in toDelete)
                {
                    try { File.Delete(path); } catch { }
                }
            }

            
            //ZipFile.CreateFromDirectory(classesFolder, @$"{Path.GetDirectoryName(pzBat)}\temp.jar", CompressionLevel.NoCompression, false);
            FastZip fastZip = new()
            {
                CreateEmptyDirectories = true,
                CompressionLevel = Deflater.CompressionLevel.NO_COMPRESSION
            };
            fastZip.CreateZip(@$"{Path.GetDirectoryName(pzBat)}\temp.jar", classesFolder, true, null);
            
            File.Copy(@$"{Path.GetDirectoryName(pzBat)}\temp.jar", zombieJar);
            File.Copy(zombieJar, classesFolder + "zombie_backup.jar");
            File.Delete(@$"{Path.GetDirectoryName(pzBat)}\temp.jar");

            string batch = $"""
                            @echo off
                            setlocal
                            
                            REM Delete the file zombie.jar if it exists
                            if exist "zombie_corrupt.jar" (
                                del /f /q "zombie_corrupt.jar"
                                echo zombie_corrupt.jar has been deleted.
                            ) else (
                                echo zombie_corrupt.jar does not exist.
                            )

                            REM Rename the first file that matches the pattern to zombie_corrupt.jar
                            for /R %%F in (zombie.jar_corrupted*.jar) do (
                                if not exist "zombie_corrupt.jar" (
                                    ren "%%F" "zombie_corrupt.jar"
                                    echo A file starting with  zombie.jar_corrupted has been renamed to zombie_corrupt.jar.
                                    goto :next
                                )
                            )

                            :next

                            REM Now, delete any remaining files that match the pattern
                            for /R %%F in (zombie.jar_corrupted*.jar) do (
                                del "%%F"
                                echo Deleted file: %%F
                            )

                            tar -xf zombie_corrupt.jar
                            start "" "{pzBatRtc}"
                            exit
                            """;

            File.WriteAllText(classesFolder + "zomboid.bat", batch);
            string pz64batText = File.ReadAllText(pzBat);
            if (cbNoVerify.Checked)
                pz64batText = pz64batText.Replace("SET _JAVA_OPTIONS=", "SET _JAVA_OPTIONS=-Xverify:none ");

            File.WriteAllText(pzBatRtc, pz64batText);

            LaunchScript script = new()
            {
                Stages =
                [
                    new()
                    {
                        Program = classesFolder + "zomboid.bat",
                        Arguments = "",
                        ShowOutput = true,
                    }
                ]
            };

            string serializedScript = JsonHelper.Serialize(script);
            File.WriteAllText(classesFolder + "zomboid.jls", serializedScript);

            _jarPath = zombieJar;
        }
    }
}
