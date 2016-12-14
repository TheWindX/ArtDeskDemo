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
    class COMArtAppMeta : CComponent
    {
        public string ID { get; set; }
        public string version { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string path { get; set; }
    }

    [RequireCom(typeof(COMArtAppMeta))]
    [RequireCom(typeof(COMListItem))]
    class COMArtApp : CComponent
    {
        UIIcon mIcon = null;
        public UIIcon UI
        {
            get
            {
                if (mIcon == null)
                {
                    mIcon = new UIIcon();
                }
                return mIcon;
            }
        }
        
        public void reflushUI()
        {
            var meta = getComponent<COMArtAppMeta>();
            var ui = UI;
            ui.title = meta.title;
            ui.setBGIMG(meta.path);
        }

        public Action reflushUIAction
        {
            get;
            set;
        }

        public COMArtApp()
        {
            reflushUIAction = reflushUI;
        }

    }

    [RequireCom(typeof(COMArtApp))]
    class COMArtAppInternal : CComponent
    {

    }

    [RequireCom(typeof(COMArtAppInternal))]
    class COMArtAppInternalStandAlone : CComponent
    {
        public void reload()
        {
            getComponent<COMArtApp>().reflushUI();
        }

        public void invoke()
        {
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName =getComponent<COMArtAppMeta>().path + "data/main.exe";
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.Start();
        }
    }

}
