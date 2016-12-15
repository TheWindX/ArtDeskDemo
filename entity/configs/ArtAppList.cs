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
        [SerializedProp]
        public List<string> tags { get; set; }
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
        const string file = "app_list.xml";
        [SerializedProp]
        public List<CArtApp> apps { get; set; }

        //取得配置文件
        void load()
        {
            string fpath = WindowsUtil.getExeDir() + file;
            var content = WindowsUtil.readTextFile(fpath);
            var instance = this as object;
            CSerializer.Instance.fromXML(ref instance, content);
        }

        void save()
        {
            string fpath = WindowsUtil.getExeDir() + file;
            var content = CSerializer.Instance.toXML(this);
            WindowsUtil.writeTextFile(fpath, content);
        }

        public List<string> getAllTags()
        {
            List<string> ts = new List<string>();
            foreach(var app in apps)
            {
                foreach(var tag in app.meta.tags)
                {
                    if(!ts.Contains(tag))
                    {
                        ts.Add(tag);
                    }
                }
            }
            return ts;
        }

        public List<CArtApp> getAllAppByTag(string tag)
        {
            List<CArtApp> r = new List<CArtApp>();
            foreach (var app in apps)
            {
                if(app.meta.tags.Contains(tag))
                {
                    r.Add(app);
                }
            }
            return r;
        }

    }
}
