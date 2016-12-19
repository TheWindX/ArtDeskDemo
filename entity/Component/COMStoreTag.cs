using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    [RequireCom(typeof(COMLister))]
    class COMStoreTag : CComponent
    {
        public string mTag = "";
        public string tag
        {
            get
            {
                return mTag;
            }
            set
            {
                mTag = value;
                getComponent<COMIcon>().getUI().title = mTag;
            }
        }
        public override void onAwake()
        {
            base.onAwake();
            var ls = getComponent<COMLister>();
            ls.doGetItem = () =>
            {
                ListEx<COMListItem> items = new ListEx<COMListItem>();
                items.pushBack(ArtBrowser.Instance.backward.getComponent<COMListItem>());
                var appConfigs = CArtAppList.Instance.getAllAppByTag(tag);
                foreach(var cfg in appConfigs)
                {
                    var appItem = instance<COMAppItem>();
                    appItem.app_config = cfg;
                    items.pushBack(appItem.getComponent<COMListItem>());
                }

                return items;
            };
        }
    }
}
