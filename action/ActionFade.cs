/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: UI淡隐现
 * */
using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ns_artDesk
{
    class ActionFade : ActionBase
    {
        double mSourceAlpha = 0;
        double mTargetAlpha = 0;

        int mTime = 0;
        int mTimeCount = 0;

        public ActionFade(float targetAlpha, int time)
        {
            mTargetAlpha = targetAlpha;
            mTime = time;
        }

        ArtUI mUI = null;
        public override void init(EntityBase entity)
        {
            base.init(entity);
            mUI = entity as ArtUI;
            mSourceAlpha = mUI.opacity;
        }

        public override void update()
        {
            if (state != EState.undating) return;
            //entity as Con
            mTimeCount += CTimeService.Instance.getDeltaTime();
            if(mTimeCount >= mTime)
            {
                mTimeCount = mTime;
                mState = EState.succeed;
            }
            var r = (double)mTimeCount / mTime;
            Trace.WriteLine("r: " + r.ToString());
            Trace.WriteLine("opacity1: " + mTargetAlpha * r + mSourceAlpha * (1 - r));
            mUI.opacity = mTargetAlpha * r + mSourceAlpha * (1 - r);
        }

        public override void cancel()
        {
            base.cancel();
            mUI.opacity = mTargetAlpha;
        }

        public override void reset()
        {
            base.reset();
            mUI = null;
            mTimeCount = 0;
        }
    }
}
