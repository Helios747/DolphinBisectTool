using SevenZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DolphinBisectTool
{
    class DownloadBuild
    {
        internal delegate void UpdateProgressDelegate(int progress_percentage, string ui_text, ProgressBarStyle progress_type);

        internal event UpdateProgressDelegate UpdateProgress;

        public DownloadBuild()
        {
            SevenZipBase.SetLibraryPath(@"7z.dll");
        }

        public void Download(string url, string major_version, int version)
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
                    this.UpdateProgress(e.ProgressPercentage, string.Format
                                       ("Downloading build {0}-{1}", major_version, version),
                                        ProgressBarStyle.Continuous);
                };

                client.DownloadFileCompleted += (s, e) =>
                {
                    // Known Bug: Sometimes the label doesn't get updated before it extracts and
                    // launches. I want to blame this meh-level 7z lib blocking something.
                    SevenZipExtractor dolphin_zip = new SevenZipExtractor(@"dolphin.7z");
                    dolphin_zip.Extracting += (sender, eventArgs) =>
                    {
                        this.UpdateProgress(eventArgs.PercentDone, "Extracting and launching", 
                                            ProgressBarStyle.Continuous);
                    };
               
                    dolphin_zip.ExtractArchive("dolphin");
                };

                client.DownloadFile(new Uri(url), "dolphin.7z");
            }
        }
    }
}
