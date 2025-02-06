using System.IO;
using System.Windows.Forms;
using FileStub;
using RTCV.CorruptCore;

namespace JavaTemplatePlugin
{
    public partial class Java : Form, IFileStubTemplate
    {
        private string _jarPath;
        public Java()
        {
            InitializeComponent();
        }

        public Form GetTemplateForm(string templateName)
        {
            return this;
        }

        public FileTarget[] GetTargets()
        {
            // stupid hack. if i don't do this, the singular jar will be imported with a MultipleFileInterface,
            // so VSPEC.OPENROMFILENAME won't have the location of the rom
            FileWatch.currentSession.selectedTargetType = TargetType.SINGLE_FILE;
            return [ new(Path.GetFileName(_jarPath), Path.GetDirectoryName(_jarPath) + '\\') ];
        }

        public void GetSegments(FileInterface exeInterface)
        {
        }

        public string[] TemplateNames => [ "Other Java Programs : JAR file" ];
        bool IFileStubTemplate.DragDrop(string[] fd)
        {
            if (fd.Length != 1 || !Path.GetFileName(fd[0]).EndsWith(".jar"))
            {
                MessageBox.Show("Please drop only one .jar file.", "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return false;
            }
            
            _jarPath = fd[0];
            return true;
        }

        public bool DisplayBrowseTarget => true;
        public bool DisplayDragAndDrop => true;
        public void BrowseFiles()
        {
            OpenFileDialog ofd = new()
            {
                Title = "Select a .jar file",
                Filter = "Java Archive|*.jar",
                Multiselect = false,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            
            _jarPath = ofd.FileName;
        }
    }
}
