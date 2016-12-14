using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    [ModuleInstance(0)]
    class LogicModule : CModule
    {
        public void onExit()
        {
            
        }

        public void onInit()
        {
            var config = Config.Instance;
        }

        public void onUpdate()
        {
            
        }
    }
}
