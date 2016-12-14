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
        public int id { get; set; }
        [SerializedProp]
        public int fold_id { get; set; }//如果fold_id 为-1，表示在根目录
        [SerializedProp]
        public int app_id { get; set; }//如果app_id为-1，表示是一个目录
    }

    class DesktopList : Singleton<DesktopList>
    {
        const string path = "desktop_list.xml";

        [SerializedProp]
        public List<DesktopItem> apps { get; set; }

        //取得配置文件
        void reload()
        {
            throw new NotImplementedException();
        }

    }
}
