/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 
 * */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core
{
    class CTimeService : Singleton<CTimeService>
    {
        Stopwatch mStopWatch = new Stopwatch();

        public CTimeService()
        {
            mStopWatch.Start();
        }

        public int getDeltaTime()
        {
            return mNow - mLast;
        }

        public int getTime()
        {
            return mNow;
        }

        public string formattedDateTime()
        {
            return mStrDT;
        }

        int mLast = 0;
        int mNow = 0;
        const string dtFormatter = "H:mm:ss";
        string mStrDT = DateTime.Now.ToString(dtFormatter);
        public void update()
        {
            mStrDT = DateTime.Now.ToString(dtFormatter);
            mLast = mNow;
            mNow = (int)mStopWatch.ElapsedMilliseconds;
        }
    }
}
