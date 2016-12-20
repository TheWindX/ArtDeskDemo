/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 文件logger
 * */
using System;
using System.IO;

namespace ns_artDesk.core
{
    class CFileLogger : Singleton<CFileLogger>, IDisposable
    {
        const string file = "log.txt";
        StreamWriter fileWriter = null;
        public CFileLogger()
        {
            if(fileWriter == null)
            {
                fileWriter = File.CreateText(WindowsUtil.getExeDir()+ file);
            }
        }
        public void error(string tag, string strFormat, params object[] values)
        {
            var strDT = CTimeService.Instance.formattedDateTime();
            var content = string.Format("ERROR:\t{0}\t{1}\t{2}", strDT, tag, string.Format(strFormat, values));
            fileWriter.WriteLine(content);
            fileWriter.Flush();
        }

        public void info(string tag, string strFormat, params object[] values)
        {
            var strDT = CTimeService.Instance.formattedDateTime();
            var content = string.Format("INFO:\t{0}\t{1}\t{2}", strDT, tag, string.Format(strFormat, values));
            fileWriter.WriteLine(content);
            fileWriter.Flush();
        }

        void IDisposable.Dispose()
        {
            if (fileWriter != null)
                fileWriter.Close();
        }
    }
}
