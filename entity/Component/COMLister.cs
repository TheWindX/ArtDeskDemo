using ns_artDesk.core;
using System;

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
                mItem = doGetItem?.Invoke();
                return mItem;
            }
        }

        public ListEx<COMListItem> currentItems()
        {
            return mItem;
        }

        internal ListEx<COMListItem> mItem = null;
        public override void onAwake()
        {
            doGetItem = () =>
            {
                if (mItem == null)
                {
                    mItem = new ListEx<COMListItem>();
                }
                return mItem;
            };

            var ui = getComponent<COMIcon>().getUI().mIcon;
            ui.Source = IconManager.Instance.folderIcon;

            getComponent<COMIcon>().getUI().evtDoubleClick += () =>
            {
                ArtBrowser.Instance.accessInto(this);
            };
        }

        public COMListItem getChoosed()
        {
            foreach(var item in currentItems())
            {
                if (item.getComponent<COMIcon>().getUI().choosed)
                    return item;
            }
            return null;
        }
    }
}
