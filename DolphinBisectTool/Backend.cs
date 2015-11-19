using SevenZip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;

namespace DolphinBisectTool
{
    class Backend
    {
        int m_min;
        int m_max;
        string m_title = "";
        // Follow this format: (Major).0
        public static string s_major_version = "4.0";
        public List<int> m_build_list = new List<int>();
        MainWindow m_form;

        public Backend()
        {
            // TODO - Replace this lib with SharpCompress
            SevenZipBase.SetLibraryPath(@"7z.dll");
        }

        public void Run()
        {
            string base_url = "https://dl.dolphin-emu.org/builds/dolphin-master-" +
                              s_major_version;
            int index = 0;

            while (m_min <= m_max)
            {
                // TODO - Find a better way to test against stable. Preferably one that allows for
                // different stables to be tested.
                if (m_min == -1)
                    index = (0 + m_max) / 2;
                else
                    index = (m_min + m_max) / 2;

                DownloadBuild(base_url + "-" + m_build_list.ElementAt(index) +
                              "-x64.7z", index);
                RunBuild();

                DialogResult dialog_result = MessageBox.Show("Testing build " +
                             m_build_list.ElementAt(index) + ". Does the issue appear?",
                             "Notice", MessageBoxButtons.YesNoCancel);

                if (dialog_result == DialogResult.Yes)
                {
                    m_max = index - 1;
                }
                else if (dialog_result == DialogResult.No)
                {
                    m_min = index + 1;
                }
                else
                {
                    m_title = "";
                    return;
                }
            }

            int broken_build = (m_build_list.ElementAt(index) + 1);
            DialogResult show_build_page =
                         MessageBox.Show("Build " + s_major_version + "-" + broken_build +
                                         " may be the cause of your issue. " +
                                         "Do you want to open the URL for that build?", "Notice",
                                         MessageBoxButtons.YesNo);

            if (show_build_page == DialogResult.Yes)
                Process.Start("https://dolp.in/" + s_major_version + "-" + broken_build);
            m_title = "";
        }

        public void SetSettings(int first, int second, MainWindow f)
        {
            m_min = first;
            m_max = second;
            m_form = f;
        }

        public void SetSettings(int first, int second, string path, MainWindow f)
        {
            m_min = first;
            m_max = second;
            m_title = path;
            m_form = f;
        }

        public void DownloadBuildList()
        {
            using (WebClient client = new WebClient())
                client.DownloadFile(new Uri("https://dl.dolphin-emu.org/builds/"),
                         "buildindex");
        }

        public void ProcessBuildList()
        {
            List<int> result = new List<int>();
            Regex regex = new Regex(@"(?<=dolphin-master-" + s_major_version + @"-)(\d{1,4})(?=-x64.7z)");

            using (StreamReader reader = new StreamReader("buildindex"))
            {
                string current_line;
                while ((current_line = reader.ReadLine()) != null)
                {
                    int stripped_build_num;
                    int.TryParse(regex.Match(current_line).Value, out stripped_build_num);
                    if (stripped_build_num != 0)
                        result.Add(stripped_build_num);
                }

                result.Sort();
                m_build_list = result;
            }
        }

        private void DownloadBuild(string url, int index)
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
            using (var download_finished = new ManualResetEvent(false))
            {
                client.DownloadProgressChanged += (s, e) =>
                {
                    m_form.ChangeProgressBar(e.ProgressPercentage, "Downloading build " +
                                             m_build_list.ElementAt(index));
                };

                client.DownloadFileCompleted += (s, e) =>
                {
                    // Known Bug: Sometimes the label doesn't get updated before it extracts and
                    // launches. I want to blame this meh-level 7z lib blocking something.
                    m_form.ChangeProgressBar(0, "Extracting and launching");
                    SevenZipExtractor dolphin_zip = new SevenZipExtractor(@"dolphin.7z");
                    dolphin_zip.ExtractArchive("dolphin");
                    download_finished.Set();
                };

                client.DownloadFileAsync(new Uri(url), "dolphin.7z");
                download_finished.WaitOne();
            }
        }

        private void RunBuild()
        {
            using (var runner = new Process())
            {
                runner.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory() +
                                                    @"\dolphin\Dolphin-x64\";
                runner.StartInfo.FileName = "Dolphin.exe";
                if (!m_title.Equals(""))
                    runner.StartInfo.Arguments = "/b /e " + "\"" + m_title + "\"";
                runner.Start();
                runner.WaitForExit();
            }
        }
    }
}
