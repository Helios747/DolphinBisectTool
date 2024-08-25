using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        internal delegate void BisectErrorDelegate(string e);
        internal event BisectErrorDelegate BisectError;

        internal delegate void UpdateProgressDelegate(int progress_percentage, string ui_text, ProgressBarStyle progress_type);
        internal event UpdateProgressDelegate UpdateProgress;

        int m_first_index;
        int m_second_index;
        readonly Dictionary<string, string> m_build_list;

        public Backend(int first_index, int second_index, Dictionary<string, string> build_list)
        {
            m_first_index = first_index;
            m_second_index = second_index;
            m_build_list = build_list;
        }

        public void Bisect(string boot_title = "")
        {
            int test_index = 0;
            int test_direction = 0;
            List<String> skipped_builds = new List<string>();
            RunBuild run_build = new RunBuild();
            Logger log = new Logger();

            while (!(m_first_index == m_second_index - 1))
            {

                test_index = m_first_index == -1 ? (0 + m_second_index) / 2 : (m_first_index + m_second_index) / 2;
                string download_url = m_build_list.ElementAt(test_index).Value;
                string download_revison = m_build_list.ElementAt(test_index).Key;
                // dumb thing to make sure we keep trying to download a build until we get a valid build
                do
                {
                    try
                    {
                        Download(download_url);
                        log.Write("Testing build " + download_revison);
                        break;
                    }
                    catch (Exception e)
                    {
                        log.Write("ERROR. Skipping build " + download_revison);
                        skipped_builds.Add(download_revison);
                        BisectError(e.Message);
                        if (test_direction == 0)
                            --test_index;
                        else
                            ++test_index;
                    }
                }
                while (true);

                if (!string.IsNullOrEmpty(boot_title))
                    run_build.Run(boot_title);
                else
                    run_build.Run();

                UserInput return_val = BisectEvent(test_index);

                if (return_val == UserInput.Yes)
                {
                    log.Write("Build " + download_revison + " marked as a BAD build");
                    m_first_index = test_index;
                    test_direction = 1;
                }
                else if (return_val == UserInput.No)
                {
                    log.Write("Build " + download_revison + " marked as a GOOD build");
                    m_second_index = test_index;
                    test_direction = 0;
                }
                else
                    return;
            }

            log.Write("Bisect completed. " + m_build_list.ElementAt(test_index).Key + " may be the culprit.");
            if (!(skipped_builds.Count == 0))
            {
                string sb = string.Join(", ", skipped_builds.ToArray());
                log.Write("Skipped builds: " + sb);
                log.Dispose();
            }
            UserInput open_url = BisectEvent(test_index, true);

            if (open_url == UserInput.Yes)
            {
                Process.Start("https://dolp.in/" + m_build_list.ElementAt(test_index - 1).Key);
            }
        }

        public void Download(string url)
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

                try
                {
                    dolphin_zip.ExtractArchive("dolphin");
                }
                catch (Exception e)
                {
                    throw new Exception("Error extracting. Probably a missing build. Skipping this build.", e);
                }
            }
        }
    }
}
