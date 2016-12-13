using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core
{
    public class CFileDownloadParamer
    {
        public String Url { get; set; }
        public String FilePath { get; set; }
        public Action<CFileDownloader> CompleteEventHandler { get; set; }
        public Action<CFileDownloader> ProgressChangedEventHandler { get; set; }
    }

    class CFileDownloadController : Singleton<CFileDownloadController>
    {
        private List<CFileDownloader> mDownloaders;

        public CFileDownloader Download(String url, String filePath, Action<CFileDownloader> completeEventHandler,
            Action<CFileDownloader> progressChangedEventHandler)
        {
            if (url == null || filePath == null)
                return null;

            CFileDownloadParamer paramter = new CFileDownloadParamer()
            {
                Url = url,
                FilePath = filePath,
                CompleteEventHandler = completeEventHandler,
                ProgressChangedEventHandler = progressChangedEventHandler
            };

            return Download(paramter);
        }

        public List<CFileDownloader> Download(String[] urls, String[] filePaths, Action<CFileDownloader> completeEventHandler,
            Action<CFileDownloader> progressChangedEventHandler)
        {
            if (urls == null || filePaths == null)
                return null;

            if (urls.Length != filePaths.Length)
                return null;

            List<CFileDownloader> downloaders = new List<CFileDownloader>();

            for (int i = 0; i < urls.Length; ++ i)
            {
                CFileDownloader downloader = this.Download(urls[i], filePaths[i], completeEventHandler, progressChangedEventHandler);
                if (downloader != null)
                {
                    downloaders.Add(downloader);
                }
            }

            return downloaders;
        }

        public List<CFileDownloader> Download(CFileDownloadParamer[] paramers)
        {
            if (paramers == null || paramers.Length <= 0)
                return null;

            List<CFileDownloader> downloaders = new List<CFileDownloader>();
            for (int i = 0; i < paramers.Length; ++ i)
            {
                CFileDownloader downloader = this.Download(paramers[i]);
                if (downloader != null)
                {
                    downloaders.Add(downloader);
                }
            }

            return downloaders;
        }

        public CFileDownloader Download(CFileDownloadParamer paramer)
        {
            if (paramer.Url == null || paramer.FilePath == null)
                return null;

            CFileDownloader downloader = new CFileDownloader(paramer.Url, paramer.FilePath);
            if (paramer.CompleteEventHandler != null)
            {
                downloader.DownloadCompletedEventHandler = paramer.CompleteEventHandler;
            }
            if (paramer.ProgressChangedEventHandler != null)
            {
                downloader.DownloadProgressChangedEventHandler = paramer.ProgressChangedEventHandler;
            }

            mDownloaders.Add(downloader);
            downloader.Start();

            return downloader;
        }

        public void init()
        {
            mDownloaders = new List<CFileDownloader>();
        }

        public void update()
        {
            List<CFileDownloader> removeItems = new List<CFileDownloader>();

            foreach (var item in mDownloaders)
            {
                if (item.DownloadProgressChangedFlag)
                {
                    if (item.DownloadProgressChangedEventHandler != null)
                    {
                        item.DownloadProgressChangedEventHandler(item);
                    }
                    
                    item.DownloadProgressChangedFlag = false;
                }

                if (item.State > FileDownloaderState.Busy)
                {
                    removeItems.Add(item);

                    if (item.DownloadCompletedEventHandler != null)
                    {
                        item.DownloadCompletedEventHandler(item);
                    }
                }
            }

            foreach (var removeItem in removeItems)
            {
                mDownloaders.Remove(removeItem);
            }
        }

        public void exit()
        {
            foreach(var item in mDownloaders)
            {
                item.Cancel();
            }

            mDownloaders.Clear();
        }
    }
}
