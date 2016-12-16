using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    [ModuleInstance(0)]
    class LogicModule : CModule
    {
        public void onExit()
        {
            
        }

        public void onInit()
        {
            //1. 加载配置
            CLogger.Instance.info("LogicModule", "config.xml is loading");
            Config.Instance.load();
            CLogger.Instance.info("LogicModule", "config.xml is loaded");
            CLogger.Instance.info("LogicModule", "app_list.xml is loading");
            CArtAppList.Instance.load();
            CLogger.Instance.info("LogicModule", "app_list.xml is loaded");
            CLogger.Instance.info("LogicModule", "desktop_list.xml is loading");
            DesktopList.Instance.load();
            CLogger.Instance.info("LogicModule", "desktop_list.xml is loaded");

            //2生成browser
            //3desktop 定位到 mainwindow.uidesk
            ArtBrowser.Instance.setDesktop();
        }

        public void onUpdate()
        {
            
        }
    }
}
