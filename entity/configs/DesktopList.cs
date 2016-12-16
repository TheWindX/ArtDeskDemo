using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    class DesktopItem
    {
        [SerializedProp]
        public string id { get; set; }

        internal const string typeFolder = "folder";
        internal const string typeApp = "app";
        [SerializedProp]
        public string type { get; set; } //"folder" or "app"
        [SerializedProp]
        public string app_id { get; set; } //if "app" type
        [SerializedProp]
        public string folder_name { get; set; } //if "folder" type

        internal const string fold_id_top = "";
        [SerializedProp]
        public string folder_id { get; set; } //if "", it is in desktop
    }

    class DesktopList : Singleton<DesktopList>
    {
        const string file = "desktop_list.xml";

        [SerializedProp]
        public List<DesktopItem> items { get; set; }

        //取得配置文件
        internal void load()
        {
            var fpath = WindowsUtil.getExeDir() + file;
            var content = WindowsUtil.readTextFile(fpath);
            var instance = this as object;
            CSerializer.Instance.fromXML(ref instance, content);
        }

        internal void save()
        {
            var fpath = WindowsUtil.getExeDir() + file;
            var content = CSerializer.Instance.toXML(this);
            WindowsUtil.writeTextFile(fpath, content);
        }
    }
}
