using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ns_artDesk.core;

namespace ns_artDesk
{
    [ModuleInstance(0)]
    class TestDeskMove : CModule
    {
        public void onExit()
        {
            
        }

        public void onInit()
        {
            CEventHub.Instance.evtKeyUp += Instance_evtKeyUp;
        }

        private void Instance_evtKeyUp(System.Windows.Input.Key k)
        {
            if(k == System.Windows.Input.Key.D0)
            {
                ArtApp.Instance.quit();
            }
            if (k == System.Windows.Input.Key.D1)
            {
                ArtDesk.Instance.MoveIn();
            }
            if (k == System.Windows.Input.Key.D2)
            {
                ArtDesk.Instance.MoveOut();
            }
        }

        public void onUpdate()
        {
            
        }
    }
}
