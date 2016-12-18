/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: UI移动动画
 * */
using ns_artDesk.core;

namespace ns_artDesk
{
    class ActionMove : ActionBase
    {
        double mSourceLeft = 0;
        double mSourceTop = 0;

        public double mTargetLeft = 0;
        public double mTagetTop = 0;

        int mTime = 0;
        int mTimeCount = 0;

        public ActionMove(float targetLeft, float tagetTop, int time)
        {
            mTargetLeft = targetLeft;
            mTagetTop = tagetTop;
            mTime = time;
        }

        ArtUI mUI = null;
        public override void init(EntityBase entity)
        {
            base.init(entity);
            mUI = entity as ArtUI;
            mSourceLeft = mUI.x;
            mSourceTop = mUI.y;
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
            var r = (float)mTimeCount / mTime;
            mUI.x = mTargetLeft * r + mSourceLeft * (1 - r);
            mUI.y = mTagetTop * r + mSourceTop * (1 - r);
        }

        public override void cancel()
        {
            base.cancel();
            mUI.x = mTargetLeft;
            mUI.y = mTagetTop;
        }

        public override void reset()
        {
            base.reset();
            mUI = null;
            mTimeCount = 0;
        }
    }
}
