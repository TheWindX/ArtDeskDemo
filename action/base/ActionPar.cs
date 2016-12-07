using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    class ActionPar : ActionBase
    {
        public enum EBreakType
        {
            e_breakOnSucc,
            e_breakOnFail,
            e_always
        }

        public EBreakType breakType = EBreakType.e_breakOnFail;
        //结构预处理
        List<ActionBase> subs = new List<ActionBase>();
        List<ActionBase> subsRunning = new List<ActionBase>();
        //重置
        public override void init(EntityBase entity)
        {
            base.init(entity);
            subsRunning.Clear();
            foreach (var sub in subs)
            {
                sub.init(entity);
                subsRunning.Add(sub);
            }
        }

        public override void reset()
        {
            base.reset();
            foreach (var sub in subs)
            {
                sub.reset();
            }
            subsRunning.Clear();
        }

        public override void update()
        {
            List<ActionBase> toRemoved = new List<ActionBase>();
            foreach (var sub in subsRunning)
            {
                sub.update();
                if (sub.state == EState.undating) continue;
                var r = sub.getResult();
                if (r)
                {
                    if (breakType == EBreakType.e_breakOnSucc)
                    {
                        mState = EState.succeed;
                        return;
                    }
                }
                else
                {   
                    if (breakType == EBreakType.e_breakOnFail)
                    {
                        mState = EState.failed;
                        return;
                    }
                }
                toRemoved.Add(sub);
            }

            foreach(var sub in toRemoved)
            {
                subsRunning.Remove(sub);
            }

            if (subsRunning.Count == 0)
            {
                mState = EState.succeed;
                return;
            }
        }

        public override void cancel()
        {
            base.cancel();
            foreach(var sub in subsRunning)
            {
                sub.cancel();
            }
        }
    }
}
