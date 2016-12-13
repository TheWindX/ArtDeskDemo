/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core
{
    public class CRandom : Singleton<CRandom>
    {
        private Random mRandGen = null;
        private void init(int seed = 0)
        {
            if(mRandGen == null)
            {
                if (seed == 0)
                    seed = Environment.TickCount;
                mRandGen = new Random(seed);
            }
        }

        public int randInt()
        {
            init();
            return mRandGen.Next();
        }

        static StringBuilder mSb = new StringBuilder();
        public string randString(int len)
        {
            init();
            mSb.Clear();
            mSb.Append(DateTime.Now.ToString("ddHHmmss"));
            for (int i = 0; i<len; ++i)
            {
                var ir = randRange(0, 25);
                var br = randBool();
                if(br)
                    mSb.Append((char)('A' + ir));
                else
                    mSb.Append((char)('a' + ir));
            }
            return mSb.ToString();
        }

        public int randRange(int min, int max)
        {
            init();
            int ri = mRandGen.Next();
            var rDet = ri % (max - min + 1);
            return min + rDet;
        }

        public bool randBool()
        {
            init();
            return mRandGen.Next()%2 == 0;
        }
    }
}
