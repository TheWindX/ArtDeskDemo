/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 
 * */

using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    abstract class EntityBase
    {
        ActionBase mAction = null;
        public virtual void setAction(ActionBase act)
        {
            if (act == null) return;
            if(mAction != null)
            {
                if (mAction.state == ActionBase.EState.undating)
                {
                    mAction.cancel();
                }
            }
            mAction = act;
            mAction.reset();
            init();
        }

        public void init()
        {
            if (mAction == null) return;
            mAction.reset();
            mAction.init(this);
        }

        public void update()
        {
            if (mAction == null) return;
            if (mAction.state == ActionBase.EState.undating)
            {
                mAction.update();
            }
        }

        public void exit()
        {
            if (mAction == null) return;
            if (mAction.state == ActionBase.EState.undating) return;
            mAction.cancel();
        }
    }
}
