using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DolphinBisectTool
{
    class Logger
    {
        StreamWriter log_file;

        public Logger()
        {
            log_file = new StreamWriter("log-" + DateTime.Now.ToString("yyyy-MM-dd_hhmmss") + ".txt");
        }

        ~Logger()
        {
            // TODO - figure out why closing the file here throws an exception.
            try
            {
                log_file.Close();
            }
            catch (Exception e)
            {
            }
        }

        public void Write(string s)
        {
            log_file.WriteLine(s);
            log_file.Flush();
        }

    }
}
