using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    [RequireCom(typeof(COMListItem))]
    [RequireCom(typeof(COMPanel))]
    class COMLister : CComponent
    {
        internal Func<ListEx<COMListItem>> doGetItem = null;

        public ListEx<COMListItem> items
        {
            get
            {
                return doGetItem?.Invoke();
            }
        }

        ListEx<COMListItem> mItem = null;
        public override void onAwake()
        {
            doGetItem = () =>
            {
                if(mItem == null)
                {
                    mItem = new ListEx<COMListItem>();
                }
                return mItem;
            };
        }
    }
}
