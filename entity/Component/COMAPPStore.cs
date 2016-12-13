using ns_artDesk.core;
using System;

namespace ns_artDesk.entity.Component
{
    class COMAPPStoreConfig : CComponent
    {
        public const string configFile = "art_desk.conf";

    }

    [RequireCom(typeof(COMAPPStoreConfig))]
    [RequireCom(typeof(COMFolder))]
    class COMAPPStore : CComponent
    {
        #region config
        
        #endregion

        //得到本机最大空间
        public string getWorkSpace()
        {
            throw new NotImplementedException();
        }

        public void reload()
        {
            throw new NotImplementedException();
        }
    }
}
