using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using SevenZip;

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
        private readonly MainWindow m_form;

        public Backend(MainWindow form)
        {
            m_form = form;
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

                DownloadBuild(base_url + "-" + m_build_list[index] +
                              "-x64.7z", index);
                RunBuild();

                DialogResult dialog_result = MessageBox.Show("Testing build " +
                             s_major_version + "-" + m_build_list[index] + ". Did the issue appear?",
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

            int broken_build = (m_build_list[index] + 1);
            DialogResult show_build_page =
                         MessageBox.Show("Build " + s_major_version + "-" + broken_build +
                                         " may be the cause of your issue. " +
                                         "Do you want to open the URL for that build?", "Notice",
                                         MessageBoxButtons.YesNo);

            if (show_build_page == DialogResult.Yes)
                Process.Start("https://dolp.in/" + s_major_version + "-" + broken_build);
            m_title = "";
        }

        public void SetSettings(int first, int second)
        {
            m_min = first;
            m_max = second;
        }

        public void SetSettings(int first, int second, string path)
        {
            m_min = first;
            m_max = second;
            m_title = path;
        }

        public void DownloadBuildList()
        {
            using (WebClient client = new WebClient())
            using (var download_finished = new ManualResetEvent(false))
            {
                client.DownloadProgressChanged += (s, e) =>
                {
                    m_form.ChangeProgressBar(e.TotalBytesToReceive > 0 ? e.ProgressPercentage : -1, "Downloading build index");
                };

                client.DownloadFileCompleted += (s, e) =>
                {
                    download_finished.Set();
                };

                client.DownloadFileAsync(new Uri("https://dl.dolphin-emu.org/builds/"),
                         "buildindex");
                download_finished.WaitOne();
            }
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
                    m_form.ChangeProgressBar(e.ProgressPercentage,
                        string.Format("Downloading build {0}-{1}", s_major_version, m_build_list[index]));
                };

                client.DownloadFileCompleted += (s, e) =>
                {
                    // Known Bug: Sometimes the label doesn't get updated before it extracts and
                    // launches. I want to blame this meh-level 7z lib blocking something.
                    m_form.ChangeProgressBar(-1, "Extracting and launching");
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
                if (!string.IsNullOrEmpty(m_title))
                    runner.StartInfo.Arguments = string.Format("/b /e \"{0}\"", m_title);
                runner.Start();
                runner.WaitForExit();
            }
        }
    }
}
