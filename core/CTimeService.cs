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
            return DateTime.Now.ToString("H:mm:ss");
        }

        int mLast = 0;
        int mNow = 0;
        public void update()
        {
            mLast = mNow;
            mNow = (int)mStopWatch.ElapsedMilliseconds;
        }
    }
}
