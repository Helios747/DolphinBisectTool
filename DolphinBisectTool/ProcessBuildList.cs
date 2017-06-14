using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DolphinBisectTool
{
    class ProcessBuildList
    {
        public List<string> Run(string s_major_version)
        {
            using (StreamReader reader = new StreamReader("buildindex"))
            {
                string raw_data = reader.ReadLine();
                string refined_data = raw_data.Replace("\"", "").Replace("[", "").Replace("]", "").Replace(" ", "");

                List<string> result = refined_data.Split(',').ToList();
                return result;
            }
        }
    }
}
