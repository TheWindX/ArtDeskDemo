using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ns_artDesk.core
{
    public enum FileDownloaderState
    {
        Ready,  
        Busy,
        Completed,
        Cancelled,
        Error,
    }

    public class CFileDownloader    
    {
        WebClient mWebClient;
        String mUri;
        String mFilePath;
        double mProgressPercent;
        long mBytesReceived;
        long mTotalBytesToReceive;
        FileDownloaderState mState;
        String mMsg;

        public String Uri { get { return mUri; } }
        public String FilePath { get { return mFilePath; } }
        public double ProgressPercent { get { return mProgressPercent; } }
        public double BytesReceived { get { return mBytesReceived; } }
        public double TotalBytesToReceive { get { return mTotalBytesToReceive; } }
        public FileDownloaderState State { get { return mState; } }
        public String Msg { get { return mMsg; } }

        public bool DownloadProgressChangedFlag { get; set; }
        public Action<CFileDownloader> DownloadProgressChangedEventHandler;
        public Action<CFileDownloader> DownloadCompletedEventHandler;
        

        public CFileDownloader(String uri, String filePath)
        {
            mWebClient = new WebClient();
            mWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.DownloadProgressChanged);
            mWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.DownloadFileCompleted);

            mUri = uri;
            mFilePath = filePath;
            mProgressPercent = 0;
            mBytesReceived = 0;
            mTotalBytesToReceive = 0;
            mState = 0;
            mMsg = "";
            DownloadProgressChangedEventHandler = null;
            DownloadCompletedEventHandler = null;
            DownloadProgressChangedFlag = false;
        }

        public bool Start()
        {
            if (this.mState == FileDownloaderState.Ready)
            {
                mWebClient.DownloadFileAsync(new Uri(mUri), mFilePath);
                mState = FileDownloaderState.Busy;
                return true;
            }

            return false;
        }

        public bool Cancel()
        {
            if (mState == FileDownloaderState.Busy)
            {
                mWebClient.CancelAsync();
                return true;
            }

            return false;
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadProgressChangedFlag = true;
            mProgressPercent = e.ProgressPercentage;
            mBytesReceived = e.BytesReceived;
            mTotalBytesToReceive = e.TotalBytesToReceive;
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                mState = e.Cancelled ? FileDownloaderState.Cancelled : FileDownloaderState.Error;
                mMsg = e.Error.Message;
            }
            else
            {
                mState = FileDownloaderState.Completed;
            }
        }
    }
}
