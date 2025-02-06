using System.Windows.Forms;
using CmlLib.Core.VersionLoader;
using CmlLib.Core.VersionMetadata;
using CmlLib.Core;
using FileStub;
using RTCV.CorruptCore;
using System.Collections.Generic;
using CmlLib.Core.Auth;
using CmlLib.Core.ProcessBuilder;
using System.IO.Compression;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace JavaTemplatePlugin
{
    public partial class Minecraft : Form, IFileStubTemplate
    {
        private string VersionsFolder = $@"{MinecraftPath.GetOSDefaultPath()}\versions\";
        public Minecraft()
        {
            InitializeComponent();
            MinecraftPath mcPath = new(MinecraftPath.GetOSDefaultPath());
            MinecraftLauncherParameters launchParams = MinecraftLauncherParameters.CreateDefault(mcPath);
            launchParams.MinecraftPath = mcPath;
            launchParams.VersionLoader = new LocalJsonVersionLoader(mcPath);

            MinecraftLauncher launcher = new(launchParams);

            var versionsTask = launcher.GetAllVersionsAsync();
            while (!versionsTask.IsCompleted) ;
            foreach (IVersionMetadata version in versionsTask.Result)
            {
                cbVersion.Items.Add(version.Name);
            }
        }

        public string[] TemplateNames => ["Minecraft Version : version.jar"];
        public bool DisplayDragAndDrop => false;

        public bool DisplayBrowseTarget => false;

        public void BrowseFiles()
        {
        }

        public void GetSegments(FileInterface exeInterface)
        {
        }

        // nightmare
        public FileTarget[] GetTargets()
        {
            if (string.IsNullOrEmpty(tbUsername.Text) || cbVersion.SelectedItem == null)
            {
                MessageBox.Show("Please ensure that there is a version selected and a username entered.", "Something's missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            string version = (string)cbVersion.SelectedItem;
            int ram = (int)nmRam.Value * 1024;
            bool noVerify = cbNoVerify.Checked;
            string name = tbUsername.Text;
            string extraArgs = tbArgs.Text ?? string.Empty;


            string folderLocation = $@"{VersionsFolder}{version}";
            if (Directory.Exists(folderLocation + "_rtc"))
            {
                Directory.Delete(folderLocation + "_rtc", true);
            }
            Directory.CreateDirectory(folderLocation + "_rtc");

            // copy everything from the original version's folder to the new _rtc folder
            DirectoryInfo source = new(folderLocation);
            DirectoryInfo target = new(folderLocation + "_rtc");
            CopyFilesRecursively(source, target);

            // create copies of the jar and delete the original
            FileInfo originalJar = new($@"{folderLocation}_rtc\{version}.jar");
            originalJar.CopyTo($@"{folderLocation}_rtc\{version}_rtc.original.jar");
            originalJar.CopyTo($@"{folderLocation}_rtc\{version}_rtc.unsigned.jar");
            originalJar.CopyTo($@"{folderLocation}_rtc\{version}_rtc.jar");
            originalJar.Delete();

            // remove the signature from the unsigned jar to make it actually unsigned
            FileInfo unsignedJar = new($@"{folderLocation}_rtc\{version}_rtc.unsigned.jar");
            using FileStream jarStream = new(unsignedJar.FullName, FileMode.Open, FileAccess.ReadWrite);
            using ZipArchive zipArchive = new(jarStream, ZipArchiveMode.Update);
            ZipArchiveEntry? entry = zipArchive.GetEntry("META-INF/MOJANGCS.SF");
            entry?.Delete();
            ZipArchiveEntry? entry2 = zipArchive.GetEntry("META-INF/MOJANGCS.RSA");
            entry2?.Delete();
            string versionJson = File.ReadAllText($@"{folderLocation}\{version}.json");
            JObject versionData = JObject.Parse(versionJson);

            // remove the "downloads" entry from the json if it exists, so that this version can be launched from the official launcher
            versionData.Remove("downloads");
            // correct the id
            versionData["id"] = "1.21.1_rtc";

            // save the json
            string newVersionJson = versionData.ToString();
            File.WriteAllText($@"{folderLocation}_rtc\{version}_rtc.json", newVersionJson);
            File.Delete($@"{folderLocation}_rtc\{version}.json");

            version += "_rtc";

            // create a minecraft process for the new version
            List<MArgument> arguments = !string.IsNullOrEmpty(extraArgs) ? extraArgs.Split(' ').Select(arg => new MArgument(arg)).ToList() : [];
            var session = MSession.CreateOfflineSession(name);
            session.UUID = "a0a0a0a0-a0a0-a0a0-a0a0-a0a0a0a0a0a0";
            MinecraftPath mcPath = new(MinecraftPath.GetOSDefaultPath());
            MinecraftLauncherParameters launchParams = MinecraftLauncherParameters.CreateDefault(mcPath);
            launchParams.MinecraftPath = mcPath;
            launchParams.VersionLoader = new LocalJsonVersionLoader(mcPath);
            MinecraftLauncher launcher = new(launchParams);
            MLaunchOption option = new()
            {
                Session = session,
                MaximumRamMb = ram,
            };
            if (noVerify)
                arguments.Add(new("-noverify"));
            option.ExtraJvmArguments = arguments;
            var processTask = launcher.CreateProcessAsync(version, option);
            while (!processTask.IsCompleted) ;
            var process = processTask.Result;

            // copy the command for the process
            string executable = process.StartInfo.FileName;
            string args = process.StartInfo.Arguments;

            // save a batch script that replaces the old version_rtc.jar with the corrupted jar
            string replaceScript = $"""
                                @echo off
                                setlocal

                                REM Delete the file {version}.jar if it exists
                                if exist "{version}.jar" (
                                    del /f /q "{version}.jar"
                                    echo {version}.jar has been deleted.
                                ) else (
                                    echo {version}.jar does not exist.
                                )

                                REM Rename the first file that matches the pattern to {version}.jar
                                for /R %%F in ({version}.unsigned.jar_corrupted*.jar) do (
                                    if not exist "{version}.jar" (
                                        ren "%%F" "{version}.jar"
                                        echo A file starting with {version}.unsigned.jar_corrupted has been renamed to {version}.jar.
                                        goto :next
                                    )
                                )

                                :next

                                REM Now, delete any remaining files that match the pattern
                                for /R %%F in ({version}.unsigned.jar_corrupted*.jar) do (
                                    del "%%F"
                                    echo Deleted file: %%F
                                )

                                endlocal
                                """;

            File.WriteAllText($@"{folderLocation}_rtc\{version}_replace.bat", replaceScript);

            // save a launch script that runs the batch script, then starts the game
            string json = JsonHelper.Serialize(new LaunchScript
            {
                Stages =
                [
                    new()
                {
                    Program = $@"{folderLocation}_rtc\{version}_replace.bat",
                    Arguments = "",
                    ShowOutput = true
                },
                new()
                {
                    Program = executable,
                    Arguments = args,
                    ShowOutput = false
                }
                ]
            });

            File.WriteAllText($@"{folderLocation}_rtc\{version}_launchscript.jls", json);
            // stupid hack. if i don't do this, the singular jar will be imported with a MultipleFileInterface,
            // so VSPEC.OPENROMFILENAME won't have the location of the rom
            FileWatch.currentSession.selectedTargetType = TargetType.SINGLE_FILE;
            return [new($"{version}.unsigned.jar", $@"{folderLocation}_rtc\")];
        }
        // https://stackoverflow.com/a/58779
        private static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
        }

        public Form GetTemplateForm(string templateName)
        {
            return this;
        }

        bool IFileStubTemplate.DragDrop(string[] fd)
        {
            return false;
        }
    }
}
