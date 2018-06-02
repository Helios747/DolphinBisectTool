using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DolphinBisectTool
{
    class Logger : IDisposable
    {
        StreamWriter log_file;
        bool disposed = false;


        public Logger()
        {
            log_file = new StreamWriter("log-" + DateTime.Now.ToString("yyyy-MM-dd_hhmmss") + ".txt");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                log_file.Close();
            }

            disposed = true;
        }

        public void Write(string s)
        {
            log_file.WriteLine(s);
            log_file.Flush();
        }

    }
}
