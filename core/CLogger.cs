/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 
 * */

namespace ns_artDesk.core
{
    class CLogger : Singleton<CLogger>
    {
        bool mFileLog = true;
        bool mConsoleLog = true;
        
        public void info(string tag, string strFormat, params object[] values)
        {
            if (mFileLog)
                CFileLogger.Instance.info(tag, strFormat, values);
            if(mConsoleLog)
                CConsoleLogger.Instance.info(tag, strFormat, values);
        }

        public void error(string tag, string strFormat, params object[] values)
        {
            if (mFileLog)
                CFileLogger.Instance.error(tag, strFormat, values);
            if (mConsoleLog)
                CConsoleLogger.Instance.error(tag, strFormat, values);
        }
    }
}
