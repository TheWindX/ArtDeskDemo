using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    class COMLister : CComponent
    {
        ListEx<COMListItem> mItem = new ListEx<COMListItem>();
        public ListEx<COMListItem> items
        {
            get
            {
                return mItem;
            }
        }
    }
}
