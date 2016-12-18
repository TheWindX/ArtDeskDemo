/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 
 * */

using ns_artDesk.core;

namespace ns_artDesk.entity
{
    class ArtAppStore : EntityBase
    {
        public static ArtAppStore Instance
        {
            get
            {
                return Singleton<ArtAppStore>.Instance;
            }
        }
    }
}
