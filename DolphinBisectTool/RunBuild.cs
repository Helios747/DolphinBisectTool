using System.Diagnostics;
using System.IO;

namespace DolphinBisectTool
{
    class RunBuild
    {
        public void Run(string title = "")
        {
            using (var runner = new Process())
            {
                string startingDirectory = Directory.GetCurrentDirectory() +
                                           @"\dolphin\Dolphin-x64\";
                runner.StartInfo.WorkingDirectory = startingDirectory;
                runner.StartInfo.FileName = startingDirectory + "Dolphin.exe";
                if (!string.IsNullOrEmpty(title))
                    runner.StartInfo.Arguments = string.Format("/b /e \"{0}\"", title);
                runner.Start();
                runner.WaitForExit();
            }
        }
    }
}
