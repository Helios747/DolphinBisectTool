using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DolphinBisectTool
{

    class Bisect
    {
        internal delegate void BisectEventDelegate(int build, bool final_trigger = false);
        internal event BisectEventDelegate BisectEvent;

        public void Run(List<int> build_list, int min, int max, string major_version, string title = "")
        {

            string base_url = "https://dl.dolphin-emu.org/builds/dolphin-master-" + major_version;
            int index = 0;
            RunBuild run_build = new RunBuild();
            DownloadBuild download_build = new DownloadBuild();


            while (min <= max)
            {
                // TODO - Find a better way to test against stable. Preferably one that allows for
                // different stables to be tested.

                index = min == -1 ? (0 + max) / 2 : (min + max) / 2;

                download_build.Download(base_url + build_list[index], major_version, build_list[index]);

                if (!string.IsNullOrEmpty(title))
                    run_build.Run(title);
                else
                    run_build.Run();

                // bisect unfinished
            }
        }
    }
}
