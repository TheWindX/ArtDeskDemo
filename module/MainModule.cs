using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    [ModuleInstance(0)]
    class MainModule : CModule
    {
        string tag = "MainModule";
        public void onExit()
        {
            
        }

        public void onInit()
        {
            //0. 拉取服务端app_list
            //1. 建立本地 local_app_list
            //2. 对于external_installaton类型，在 program data下检查app_list是否有新安装， 建立本地app目录，更新app state
            //3. 本地app_list检查版本，刷新app state
            

            //加载配置
            Config.Instance.load();
            CLogger.Instance.info(tag, "config.xml is loaded");
            CArtAppList.Instance.load();
            CLogger.Instance.info(tag, "app_list.xml is loaded");
            DesktopList.Instance.load();
            CLogger.Instance.info(tag, "desktop_list.xml is loaded");

            //生成browser
            //3desktop 定位到 mainwindow.uidesk
            ArtBrowser.Instance.setView();
        }

        public void onUpdate()
        {
            
        }
    }
}
