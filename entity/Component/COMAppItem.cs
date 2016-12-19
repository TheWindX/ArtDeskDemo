using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ns_artDesk.core;
using ns_artDesk.view.widget;

namespace ns_artDesk
{
    [RequireCom(typeof(COMListItem))]
    class COMAppItem : CComponent
    {
        public CArtApp app_config
        {
            get;
            set;
        }

        public DesktopItem mDesktop_config;
        public DesktopItem desktop_config
        {
            get
            {
                return mDesktop_config;
            }
            set
            {
                mDesktop_config = value;
                foreach (var app in CArtAppList.Instance.apps)
                {
                    if (app.meta.name == desktop_config.app_id)
                    {
                        app_config = app;
                    }
                }
            }
        }


        public override void onAwake()
        {
            var icon = getComponent<COMIcon>();
            icon.doGetUI = () =>
            {
                var ui = icon.doGetUIBase();
                ui.title = app_config.meta.title;
                return ui;
            };
        }

        public void run()
        {

        }
    }
}
