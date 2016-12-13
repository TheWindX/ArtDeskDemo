/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: action基类
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    abstract class ActionBase
    {
        public enum EState
        {
            uninit,
            undating,
            failed,
            succeed,
            cancelled,
        }

        protected EState mState = EState.uninit;
        public EState state
        {
            get
            {
                return mState;
            }
        }

        protected EntityBase mEntity = null;
        public virtual void init(EntityBase entity)
        {
            mState = EState.undating;
            mEntity = entity;
        }

        public virtual void update()
        {
            mState = EState.succeed;
        }

        public virtual void cancel()
        {
            mState = EState.cancelled;
        }

        public virtual void reset()
        {
            mState = EState.uninit;
        }

        public virtual bool getResult()
        {
            if (mState == EState.succeed)
                return true;
            return false;
        }
    }
}
