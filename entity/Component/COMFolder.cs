using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    [RequireCom(typeof(COMLister))]
    class COMFolder : CComponent
    {
        public DesktopItem config { get; set; }
        public override void onAwake()
        {
            base.onAwake();
            var ls = getComponent<COMLister>();
            ls.doGetItem = () =>
            {
                ListEx<COMListItem> r = new ListEx<COMListItem>();
                var fid = config.id;

                var item = instance<COMUpward>().getComponent<COMListItem>();
                r.pushBack(item);

                foreach (var conf in DesktopList.Instance.items)
                {
                    if (conf.folder_id == fid)
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
                ui.title = config.folder_name;
                return ui;
            };
        }
    }
}
