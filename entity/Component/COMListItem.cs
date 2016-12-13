using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    class COMListItem : CComponent
    {
        COMFolder mLocation = null;
        public COMFolder location
        {
            get
            {
                return mLocation;
            }

            set
            {
                if (value == null) return;
                if (value.getComponent<COMListItem>() == this) return;
                if(mLocation != null)
                {
                    mLocation.getComponent<COMLister>().items.remove(this);
                    this.mLocation = value;
                    value.getComponent<COMLister>().items.pushBack(this);
                }
            }
        }

        public string name
        {
            get;
            set;
        }
        public string URL
        {
            get
            {
                if(mLocation == null)
                {
                    return "";
                }
                else
                {
                    var p = mLocation.getComponent<COMListItem>();
                    return p.URL + "/" + name;
                }
            }
        }
    }
}
