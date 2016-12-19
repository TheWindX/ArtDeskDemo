using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    class ArtBrowser : EntityBase
    {
        public static ArtBrowser Instance
        {
            get
            {
                return Singleton<ArtBrowser>.Instance;
            }
        }

        Stack<COMLister> history
        {
            get;set;
        }

        COMLister mRoot = null;
        public COMLister root
        {
            get
            {
                return mRoot;
            }
        }

        COMStoreFolder mStore = null;
        public COMStoreFolder store
        {
            get
            {
                return mStore;
            }
        }

        COMDesktop mDestop = null;
        public COMDesktop destop
        {
            get
            {
                return mDestop;
            }
        }

        COMUpward mBackward = null;
        public COMUpward backward
        {
            get
            {
                return mBackward;
            }
        }

        public ArtBrowser()
        {
            mRoot = CComponent.instance<COMLister>();
            mStore = CComponent.instance<COMStoreFolder>();
            mDestop = CComponent.instance<COMDesktop>();
            mBackward = CComponent.instance<COMUpward>();
            mRoot.items.pushBack(backward.getComponent<COMListItem>());
            mRoot.items.pushBack(mDestop.getComponent<COMListItem>());
            mRoot.items.pushBack(mStore.getComponent<COMListItem>());
            history = new Stack<COMLister>();
            history.Push(mRoot);
        }

        public COMLister current
        {
            get;
            private set;
        }

        public void accessInto(COMLister ls)
        {
            history.Push(current);
            current = ls;
            current.getComponent<COMPanel>().setDesktop();
        }

        public void exitFrom()
        {
            var last = history.Pop();
            current = last;
            current.getComponent<COMPanel>().setDesktop();
        }

        public void setDesktop()
        {
            accessInto(destop.getComponent<COMLister>());
        }
    }
}
