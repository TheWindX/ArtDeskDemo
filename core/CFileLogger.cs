/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 文件logger
 * */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core
{
    class CFileLogger : Singleton<CFileLogger>, IDisposable
    {
        const string outputPath = "log.txt";
        StreamWriter fileWriter = null;
        public CFileLogger()
        {
            if(fileWriter == null)
            {
                fileWriter = File.AppendText(outputPath);
            }
        }
        public void error(string tag, string strFormat, params object[] values)
        {
            var strDT = CTimeService.Instance.formattedDateTime();
            var content = string.Format("error:\t{0}\t{1}", tag, strFormat, string.Format(strFormat, values));
            fileWriter.WriteLine(content);
            fileWriter.Flush();
        }

        public void info(string tag, string strFormat, params object[] values)
        {
            var strDT = CTimeService.Instance.formattedDateTime();
            var content = string.Format("info:\t{0}\t{1}", tag, strFormat, string.Format(strFormat, values));
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
