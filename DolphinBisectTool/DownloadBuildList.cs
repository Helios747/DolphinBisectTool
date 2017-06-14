using System;
using System.Net;
using System.Windows.Forms;

namespace DolphinBisectTool
{
    class DownloadBuildList
    {
        internal delegate void UpdateProgressDelegate(int progress_percentage, string ui_text, ProgressBarStyle progress_type);

        internal event UpdateProgressDelegate UpdateProgress;

        public void Download()
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += (s, e) =>
                {
                    this.UpdateProgress(e.TotalBytesToReceive > 0 ? e.ProgressPercentage : -1, "Downloading build index",
                                        ProgressBarStyle.Marquee);
                };

                client.DownloadFileCompleted += (s, e) =>
                {
                    this.UpdateProgress(100, "Parsing build list", ProgressBarStyle.Marquee);
                };

                client.DownloadFileAsync(new Uri("https://www.dolphin-emu.org/download/buildlist"), "buildindex");
            }
        }
    }
}
