using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{
    [ModuleInstance(0)]
    class ConsoleModule : CModule
    {
        public void onExit()
        {
            CSRepl.stop();
        }

        public void onInit()
        {
            CSRepl.Instance.start();
        }

        public void onUpdate()
        {
            CSRepl.Instance.runOnce();
        }
    }
}
