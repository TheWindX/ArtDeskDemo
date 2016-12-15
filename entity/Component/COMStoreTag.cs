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
        public string tag { get; set; }
        public override void onAwake()
        {
            base.onAwake();
            var ls = getComponent<COMLister>();
            ls.doGetItem = () =>
            {
                ListEx<COMListItem> items = new ListEx<COMListItem>();
                var appConfigs = CArtAppList.Instance.getAllAppByTag(tag);
                foreach(var cfg in appConfigs)
                {
                    var appItem = instance<COMAppItem>();
                    appItem.app_config = cfg;
                }

                return items;
            };
        }
    }
}
