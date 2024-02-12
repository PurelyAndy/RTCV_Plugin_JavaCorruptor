using RTCV.CorruptCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAVACORRUPTOR
{
    public class JavaBlastLayer
    {
        public Dictionary<string, List<JavaBlastUnit>> layer = new Dictionary<string, List<JavaBlastUnit>>();

        //getenumerator
        public IEnumerator<KeyValuePair<string, List<JavaBlastUnit>>> GetEnumerator() => layer.GetEnumerator();
        public void Add(string key, List<JavaBlastUnit> value) => layer.Add(key, value);
    }
}
