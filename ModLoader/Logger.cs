using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader
{
    public class Logger
    {
        public static void Write(string text)
        {
            #if DEBUG
            Console.Write(text);
            #endif
        }
    }
}
