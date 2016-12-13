/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: action串行组合
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ns_artDesk.core;

namespace ns_artDesk
{
    class ActionSeq : ActionBase
    {
        public enum EBreakType
        {
            e_breakOnSucc,
            e_breakOnFail,
            e_always
        }
        public EBreakType breakType = EBreakType.e_breakOnFail;

        List<ActionBase> mActs = new List<ActionBase>();
        public void addAction(ActionBase act)
        {
            mActs.Add(act);
        }

        public override void init(EntityBase entity)
        {
            base.init(entity);
            mActionIdx = 0;
            if(mActionIdx < mActs.Count)
                mActs[mActionIdx].init(entity);
        }
        int mActionIdx = int.MaxValue;
        public override void update()
        {
            if(mState != EState.undating)
            {
                CLogger.Instance.error("Action",  "ActionSeq must be updating mState");
                throw new Exception();
                //return;
            }
            for (; true;)
            {
                if (mActionIdx >= mActs.Count)
                {
                    if(breakType == EBreakType.e_breakOnFail)
                        mState = EState.succeed;
                    else if (breakType == EBreakType.e_breakOnSucc)
                        mState = EState.failed;
                    else if (breakType == EBreakType.e_always)
                        mState = EState.succeed;
                    return;
                }
                ActionBase s = mActs[mActionIdx];
                s.update();
                if (s.state == EState.undating)
                {
                    return;
                }
                if (breakType == EBreakType.e_breakOnFail)
                {
                    if(s.state == EState.failed)
                    {
                        mState = EState.failed;
                        return;
                    }
                    else if(s.state == EState.succeed)
                    {
                        mActionIdx++;
                    }
                    else
                    {
                        CLogger.Instance.error("Action", "child in ActionSeq is in {0} mState", s.state.ToString());
                        throw new Exception();
                    }
                }
                else if (breakType == EBreakType.e_breakOnSucc)
                {
                    if (s.state == EState.succeed)
                    {
                        mState = EState.succeed;
                        return;
                    }
                    if (s.state == EState.failed)
                    {
                        mActionIdx++;
                    }
                    else
                    {
                        CLogger.Instance.error("Action", "child in ActionSeq is in {0} mState", s.state.ToString());
                        throw new Exception();
                    }
                }
                else if (breakType == EBreakType.e_always)
                {
                    if (s.state == EState.succeed)
                    {
                        mActionIdx++;
                    }
                    else if (s.state == EState.failed)
                    {
                        mActionIdx++;
                    }
                    else
                    {
                        CLogger.Instance.error("Action", "child in ActionSeq is in {0} mState", s.state.ToString());
                        throw new Exception();
                    }
                }
                s = mActs[mActionIdx];
                s.init(mEntity);
            }
        }

        public override void cancel()
        {
            if (mState != EState.undating) return;
            base.cancel();
            if (mActionIdx < mActs.Count)
                mActs[mActionIdx].cancel();
        }

        public override void reset()
        {
            foreach(var act in mActs)
            {
                act.reset();
            }
            mActionIdx = int.MaxValue;
        }
    }
}
