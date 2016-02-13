using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace DolphinBisectTool
{
    class RunBuild
    {
        public void Run(string title = "")
        {
            using (var runner = new Process())
            {
                runner.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory() +
                                                    @"\dolphin\Dolphin-x64\";
                runner.StartInfo.FileName = "Dolphin.exe";
                if (!string.IsNullOrEmpty(title))
                    runner.StartInfo.Arguments = string.Format("/b /e \"{0}\"", title);
                runner.Start();
                runner.WaitForExit();
            }
        }
    }
}
