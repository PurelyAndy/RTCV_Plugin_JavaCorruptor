using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaTemplatePlugin
{
    internal class LaunchScript
    {
        public List<ScriptStage> Stages { get; set; } = [];
        public LaunchScript() { }
        public class ScriptStage
        {
            public string Program { get; set; }
            public string Arguments { get; set; }
            public bool ShowOutput { get; set; }
        }
    }
}
