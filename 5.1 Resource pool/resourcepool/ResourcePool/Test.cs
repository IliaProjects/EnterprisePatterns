using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ResourcePool
{
    internal class Test
    {
        private static int sec = 0;
        private static int _seconds
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get {
                Console.WriteLine("GETTED");
                return sec;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set {
                Console.WriteLine("PUTTED");
                sec = value;
            }
        }

        public static void get()
        {
            int seconds = _seconds;
        }

        public static void put()
        {
            _seconds = new Random().Next(0, 100);
        }
    }
}
