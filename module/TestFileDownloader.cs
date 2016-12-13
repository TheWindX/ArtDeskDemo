using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ns_artDesk.module
{
    [ModuleInstance(1)]
    class TestFileDownloader : CModule
    {
        public void onExit()
        {

        }

        public void onInit()
        {
            CEventHub.Instance.evtGlobalKeyUp += Instance_evtGlobalKeyUp;
            
        }

        private void Instance_evtGlobalKeyUp(System.Windows.Input.Key k)
        {
            Trace.WriteLine(k.ToString());
            if (k == System.Windows.Input.Key.Left)
            {
                CFileDownloadController.Instance.Download(
                    "http://172.18.119.83:9000/media/hello.txt",
                    "hello2.txt",
                    new Action<CFileDownloader>(this.DownloadCompleted),
                    new Action<CFileDownloader>(this.DownloadProgressChanged));
            }
        }

        public void onUpdate()
        {

        }

        private void DownloadProgressChanged(CFileDownloader downloader)
        {
            Trace.WriteLine(string.Format("DownloadProgressChanged {0} {1}:{2}  {3}",
                downloader.FilePath, downloader.BytesReceived,
                downloader.TotalBytesToReceive, downloader.ProgressPercent));
        }

        private void DownloadCompleted(CFileDownloader downloader)
        {
            Trace.WriteLine(string.Format("DownloadProgressChanged {0} {1}",
                downloader.FilePath, downloader.State.ToString()));
        }
    }
}
