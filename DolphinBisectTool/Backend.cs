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
    }
}
