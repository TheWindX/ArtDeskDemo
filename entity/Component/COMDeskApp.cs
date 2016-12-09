using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    [RequireCom(typeof(COMDeskItem))]
    class COMDeskApp : CComponent
    {
        public string appID
        {
            get;
            set;
        }

        public string desc
        {
            get;
            set;
        }

        public string version
        {
            get;
            set;
        }
    }
}
