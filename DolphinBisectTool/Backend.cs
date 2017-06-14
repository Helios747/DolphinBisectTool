using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using SevenZip;

namespace DolphinBisectTool
{
    public enum UserInput
    {
        Yes,
        No,
        Cancel
    }

    class Backend
    {
        internal delegate UserInput BisectEventDelegate(int build, bool final_trigger = false);
        internal event BisectEventDelegate BisectEvent;

        internal delegate void UpdateProgressDelegate(int progress_percentage, string ui_text, ProgressBarStyle progress_type);
        internal event UpdateProgressDelegate UpdateProgress;

        int m_first_index;
        int m_second_index;
        List<string> m_build_list;

        public Backend(int first_index, int second_index, List<string> build_list)
        {
            m_first_index = first_index;
            m_second_index = second_index;
            m_build_list = build_list;
        }

        public void Bisect(string boot_title = "")
        {
            string base_url = "https://dl.dolphin-emu.org/builds/dolphin-master-";
            int test_index = 0;
            RunBuild run_build = new RunBuild();

            while (m_first_index <= m_second_index)
            {

                test_index = m_first_index == -1 ? (0 + m_second_index) / 2 : (m_first_index + m_second_index) / 2;

                Download(base_url + m_build_list[test_index] + "-x64.7z", m_build_list[test_index]);

                if (!string.IsNullOrEmpty(boot_title))
                    run_build.Run(boot_title);
                else
                    run_build.Run();

                UserInput return_val = BisectEvent(test_index);

                if (return_val == UserInput.Yes)
                    m_second_index = test_index - 1;
                else if (return_val == UserInput.No)
                    m_first_index = test_index + 1;
                else
                    return;
            }

            UserInput open_url = BisectEvent(test_index, true);

            if (open_url == UserInput.Yes)
            {
                Process.Start("https://dolp.in/" + m_build_list[test_index+1]);
            }
        }

        public void Download(string url, string version)
        {
            // Windows will throw an error if you have the folder you're trying to delete open in
            // explorer. It will remove the contents but error out on the folder removal. That's
            // good enough but this is just so it doesn't crash.
            try
            {
                if (Directory.Exists(@"dolphin"))
                    Directory.Delete(@"dolphin", true);
            }
            catch (IOException)
            {
            }

            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += (s, e) =>
                {
                    UpdateProgress(e.ProgressPercentage, "Downloading build", ProgressBarStyle.Continuous);
                };
                client.DownloadFileAsync(new Uri(url), "dolphin.7z");

                while (client.IsBusy)
                {
                    Application.DoEvents();
                }

                SevenZipExtractor dolphin_zip = new SevenZipExtractor(@"dolphin.7z");

                dolphin_zip.Extracting += (sender, eventArgs) =>
                {
                    UpdateProgress(eventArgs.PercentDone, "Extracting and launching", ProgressBarStyle.Continuous);
                };
                dolphin_zip.ExtractArchive("dolphin");
            }
        }
    }
}
