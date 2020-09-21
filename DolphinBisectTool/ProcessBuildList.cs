using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DolphinBisectTool
{
    class ProcessBuildList
    {
        public Dictionary<string, string> Run()
        {
            using (StreamReader reader = new StreamReader("buildindex"))
            {
                string raw_data = reader.ReadLine();
                string refined_data = raw_data.Replace("\"", "").Replace("[", "").Replace("]", "").Replace(" ", "");

                Dictionary<string, string> result = new Dictionary<string, string>();
                string[] items = refined_data.Split(',');

                for (int i = 0; i < items.Length; i += 2)
                {
                    // A key without a value *should* be impossible, however this prevents a crash.
                    if (i + 2 > items.Length)
                        continue;

                    if (result.ContainsKey(items[i]))
                        continue; // There are two version 4.0-390s. 

                    result.Add(items[i], items[i + 1]);
                }

                return result;
            }
        }
    }
}
