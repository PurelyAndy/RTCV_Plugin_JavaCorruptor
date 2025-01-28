using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Java_Corruptor
{
    internal static class CorruptionOptions
    {
        public static int MethodCompute = 1;
        public static bool CompressJar = false;
        public static int Threads = 4;
        public static bool UseDomains = false;
        public static List<string> FilterClasses = [];
        public static List<string> FilterMethods = [];
    }
}
