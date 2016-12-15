using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    [RequireCom(typeof(COMFolder))]
    class COMDesktop : CComponent
    {
        public override void onAwake()
        {
            base.onAwake();
            var list = getComponent<COMLister>();
            list.doGetItem = () =>
            {
                ListEx<COMListItem> r = new ListEx<COMListItem>();
                foreach(var conf in DesktopList.Instance.items)
                {
                    if(conf.folder_id == DesktopItem.fold_id_top)
                    {
                        if (conf.type == DesktopItem.typeFolder)
                        {
                            var folder = instance<COMFolder>();
                            folder.config = conf;
                            r.pushBack(folder.getComponent<COMListItem>());
                        }
                        else if (conf.type == DesktopItem.typeApp)
                        {
                            var app = instance<COMAppItem>();
                            app.desktop_config = conf;
                            r.pushBack(app.getComponent<COMListItem>());
                        }
                    }
                }
                return r;
            };

            var icon = getComponent<COMIcon>();
            icon.doGetUI = () =>
            {
                var ui = icon.doGetUIBase();
                ui.title = "桌面";
                return ui;
            };
        }
    }
}
