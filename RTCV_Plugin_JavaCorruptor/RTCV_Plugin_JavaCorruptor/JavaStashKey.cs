using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAVACORRUPTOR
{
    [Serializable]
    internal class JavaStashKey : INote
    {
        private string _alias;

        public string JarFilename { get; set; }
        public string JarShortFilename { get; set; }
        public string GameName { get; set; }
        public string Note { get; set; }

        public string Key { get; set; }
        public string ParentKey { get; set; }
        public BlastLayer BlastLayer { get; set; }

        public string Alias
        {
            get => _alias ?? Key;
            set => _alias = value;
        }

        public JavaStashKey()
        {
            var key = RtcCore.GetRandomKey();
            string parentkey = null;
            BlastLayer blastlayer = new BlastLayer();
            StashKeyConstructor(key, parentkey, blastlayer);
        }

        public JavaStashKey(string key, string parentkey, BlastLayer blastlayer)
        {
            StashKeyConstructor(key, parentkey, blastlayer);
        }

        private void StashKeyConstructor(string key, string parentkey, BlastLayer blastlayer)
        {
            Key = key;
            ParentKey = parentkey;
            BlastLayer = blastlayer;

            JarFilename = (string)AllSpec.VanguardSpec?[VSPEC.OPENROMFILENAME] ?? "ERROR";
            JarShortFilename = Path.GetFileName(JarFilename);
            GameName = JarShortFilename.Substring(0, JarShortFilename.LastIndexOf('.'));
        }

        public override string ToString()
        {
            return Alias;
        }
    }
}
