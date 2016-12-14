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
        COMFolder mRoot = null;
        public COMFolder root
        {
            get
            {
                return mRoot;
            }
        }

        COMFolder mStore = null;
        public COMFolder store
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
            COMObj obj = new COMObj() { name = "root" };
            mRoot = obj.addComponent<COMFolder>().Last() as COMFolder;

            //增加app store
            obj = new COMObj() { name = "store" };
            mStore = obj.addComponent<COMFolder>().Last() as COMFolder;
            mRoot.appItem(mStore.getComponent<COMListItem>());

            //增加destop
            obj = new COMObj() { name = "desktop" };
            mDestop = obj.addComponent<COMDesktop>().Last() as COMDesktop;
            mRoot.appItem(mDestop.getComponent<COMListItem>());
        }

        public void reload()
        {

        }
    }
}
