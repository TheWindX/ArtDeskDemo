using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    public class CArtAppMeta
    {
        [SerializedProp]
        public string name { get; set; }
        [SerializedProp]
        public double version { get; set; }
        [SerializedProp]
        public string title { get; set; }
        [SerializedProp]
        public string desc { get; set; }
        [SerializedProp]
        public string type { get; set; }
    }

    public class CArtApp
    {
        [SerializedProp]
        public string path { get; set; }

        [SerializedProp]
        public CArtAppMeta meta { get; set; }
    }


    public class CArtAppList : Singleton<CArtAppList>
    {
        [SerializedProp]
        public List<CArtApp> apps { get; set; }

        //取得配置文件
        void reload()
        {
            throw new NotImplementedException();
        }

    }
}
