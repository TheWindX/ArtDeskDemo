using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.View
{
    class FrameTop : EntityBase
    {
        public static FrameTop Instance
        {
            get
            {
                return Singleton<FrameTop>.Instance;
            }
        }
    }
}
