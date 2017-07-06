using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Logger
    {
        static string _fileName;

        public static void Message(string msg)
        {
            File.AppendAllText(_fileName +".txt", msg);
        }
        
        static Logger()
        {
            _fileName = "ServerLog_" + DateTime.Now.Date.ToString("dd_mm_yy");
        }
    }
}
