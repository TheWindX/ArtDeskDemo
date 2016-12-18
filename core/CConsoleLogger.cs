/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 命令行LOG
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core
{
    class CConsoleLogger : Singleton<CConsoleLogger>
    {
        public void error(string tag, string strFormat, params object[] values)
        {
            var strDT = CTimeService.Instance.formattedDateTime();
            var content = string.Format("ERROR:\t{0}\t{1}\t{2}", strDT, tag, string.Format(strFormat, values));
            Console.WriteLine(content);
        }

        public  void info(string tag, string strFormat, params object[] values)
        {
            var strDT = CTimeService.Instance.formattedDateTime();
            var content = string.Format("INFO:\t{0}\t{1}\t{2}", strDT, tag, string.Format(strFormat, values));
            Console.WriteLine(content);
        }
    }
}
