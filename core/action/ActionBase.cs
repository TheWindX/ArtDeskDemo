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

        public virtual void init()
        {
            mState = EState.undating;
        }

        public virtual void update(EntityBase entity)
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
