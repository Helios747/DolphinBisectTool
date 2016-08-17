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
                string starting_directory = Directory.GetCurrentDirectory() +
                                            @"\dolphin\Dolphin-x64\";
                runner.StartInfo.WorkingDirectory = starting_directory;
                runner.StartInfo.FileName = starting_directory + "Dolphin.exe";
                if (!string.IsNullOrEmpty(title))
                    runner.StartInfo.Arguments = string.Format("/b /e \"{0}\"", title);
                runner.Start();
                runner.WaitForExit();
            }
        }
    }
}
