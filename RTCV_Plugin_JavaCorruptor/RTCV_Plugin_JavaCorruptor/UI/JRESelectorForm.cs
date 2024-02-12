using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JAVACORRUPTOR.UI
{
    public partial class JRESelectorForm : Form
    {
        public JRESelectorForm()
        {
            InitializeComponent();
        }

        static void CopyDirectory(string sourceDir, string destinationDir) // Thank you, Microsoft docs!
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                CopyDirectory(subDir.FullName, newDestinationDir);
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            string pluginsDir = $@"{Directory.GetCurrentDirectory()}\RTC\PLUGINS\";
            if (lb32Bit.SelectedIndex == -1 && lb64Bit.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a runtime to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string path;
            if (lb64Bit.SelectedIndex != -1)
                path = lb64Bit.SelectedItem.ToString();
            else
                path = lb32Bit.SelectedItem.ToString();

            if (!Directory.Exists($@"{pluginsDir}\jre"))
                Directory.CreateDirectory($@"{pluginsDir}\jre");
            else
            {
                // Recursively set all of the files to not read-only
                foreach (string file in Directory.GetFiles($@"{pluginsDir}\jre", "*.*", SearchOption.AllDirectories))
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                }
                Directory.Delete($@"{pluginsDir}\jre", true);
                Directory.CreateDirectory($@"{pluginsDir}\jre");
            }

            string destination = $@"{pluginsDir}\jre\";
            if (Directory.Exists($@"{path}\jre\bin"))
            {
                string source = $@"{path}\jre\";
                CopyDirectory(source + @"bin\", destination + @"bin\");
                CopyDirectory(source + @"lib\", destination + @"lib\");
            }
            else
            {
                string source = $@"{path}\";
                CopyDirectory(source + @"bin\", destination + @"bin\");
                CopyDirectory(source + @"lib\", destination + @"lib\");
            }

            File.Create($@"{pluginsDir}\jrechecked");

            byte[] bytes = { 0x6A, 0x72, 0x65, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            using (Stream stream = File.Open($@"{pluginsDir}\JavaCorruptor_packed.exe", FileMode.Open))
            {
                stream.Position = 0xEBA8;
                stream.Write(bytes, 0, bytes.Length); //Replaces the "%JAVA_HOME%;%PATH%" string with "jre" and zeroes out the rest of the string.
            }

            Close();
        }

        private void lb64Bit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lb32Bit.SelectedIndex = -1;
        }

        private void lb32Bit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lb64Bit.SelectedIndex = -1;
        }
    }
}
