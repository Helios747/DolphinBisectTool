using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DolphinBisectTool
{
    class ProcessBuildList
    {
        public List<int> Run(string s_major_version)
        {
            List<int> result = new List<int>();
            Regex regex = new Regex(@"(?<=dolphin-master-" + s_major_version + @"-)(\d{1,4})(?=-x64.7z)");

            using (StreamReader reader = new StreamReader("buildindex"))
            {
                string current_line;
                while ((current_line = reader.ReadLine()) != null)
                {
                    int stripped_build_num;
                    if (int.TryParse(regex.Match(current_line).Value, out stripped_build_num))
                    {
                        result.Add(stripped_build_num);
                    }
                }

                result.Sort();
                return result;
            }
        }
    }
}
