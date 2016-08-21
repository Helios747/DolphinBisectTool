using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DolphinBisectTool
{
    class RunBuild
    {
        public void Run(string title = "")
        {
            string starting_directory = Directory.GetCurrentDirectory() + @"\dolphin\Dolphin-x64\";                                                                                                    
            string[] files = Directory.GetFiles(starting_directory).Select(file =>
                             Path.GetFileName(file)).ToArray();
            string pattern = @"(^Dolphin.*)";
            var match = files.Where(path => Regex.Match(path, pattern).Success);

            using (var runner = new Process())
            {
                runner.StartInfo.WorkingDirectory = starting_directory;
                // This is probably gross.
                runner.StartInfo.FileName = starting_directory + string.Join("", match);
                runner.StartInfo.UseShellExecute = false;
                if (!string.IsNullOrEmpty(title))
                    runner.StartInfo.Arguments = string.Format("/b /e \"{0}\"", title);
                runner.Start();
                runner.WaitForExit();
            }
        }
    }
}
