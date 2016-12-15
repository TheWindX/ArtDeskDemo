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
        
        public ArtBrowser()
        {
            mRoot = CComponent.instance<COMLister>();
            mDestop = CComponent.instance<COMDesktop>();
            mStore = CComponent.instance<COMStoreFolder>();

            mRoot.items.pushBack(mDestop.getComponent<COMListItem>());
            mRoot.items.pushBack(mStore.getComponent<COMListItem>());
        }

        public void reload()
        {

        }
    }
}
