using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    class Config : Singleton<Config>
    {
        const string file = "config.xml";
        const string workSpaceDir = "WS";

        [SerializedProp]
        public string work_space_dir { get; set; }

        [SerializedProp]
        public string http_server { get; set; }

        public Config()
        {
            load();
        }

        void load()
        {
            string filePath = WindowsUtil.getExeDir() + file;
            var fi = new FileInfo(filePath);
            if (!fi.Exists)
            {
                //初次创建
                var fs = File.Create(filePath);
                fs.Close();
                //在最大分区创建工作目录
                var di = WindowsUtil.getMaxFreeSpaceDriverDir();
                di = new DirectoryInfo(di + workSpaceDir);
                if (!di.Exists)
                {
                    di.Create();
                }
                work_space_dir = di.FullName + "/";
                var content = CSerializer.Instance.toXML(this);
                File.WriteAllText(fi.FullName, content);
            }
            else
            {
                var content = WindowsUtil.readTextFile(fi.FullName);
                object instance = this;
                CSerializer.Instance.fromXML(ref instance, content);
            }
        }
    }
}
