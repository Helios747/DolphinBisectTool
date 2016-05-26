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
    public enum UserInput
    {
        Yes,
        No,
        Cancel
    }

    class Backend
    {
        internal delegate void BisectEventDelegate(int build, bool final_trigger = false);
        internal event BisectEventDelegate BisectEvent;

        int m_first_index;
        int m_second_index;
        string m_major_version;
        List<int> m_build_list;
        
        public Backend(int first_index, int second_index, List<int> build_list, string major_version)
        {
            m_first_index = first_index;
            m_second_index = second_index;
            m_build_list = build_list;
            m_major_version = major_version;
        }

        public void Bisect(string boot_title = "")
        {
            string base_url = "https://dl.dolphin-emu.org/builds/dolphin-master-" + m_major_version;
            int test_index = 0;
            RunBuild run_build = new RunBuild();
            DownloadBuild download_build = new DownloadBuild();

            while (m_first_index <= m_second_index)
            {

                test_index = m_first_index == -1 ? (0 + m_second_index) / 2 : (m_first_index + m_second_index) / 2;

                download_build.Download(base_url + m_build_list[test_index], m_major_version, m_build_list[test_index]);

                if (!string.IsNullOrEmpty(boot_title))
                    run_build.Run(boot_title);
                else
                    run_build.Run();

                UserInput return_val = BisectEvent(test_index);

            }


        }
    }
}
