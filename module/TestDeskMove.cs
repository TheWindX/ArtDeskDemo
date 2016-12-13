/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ns_artDesk.core;
using System.Diagnostics;

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
            CEventHub.Instance.evtGlobalKeyUp += Instance_evtGlobalKeyUp;
        }

        private void Instance_evtGlobalKeyUp(System.Windows.Input.Key k)
        {
            Trace.WriteLine(k.ToString());
            if(k == System.Windows.Input.Key.D0)
            {
                ArtFrame.Instance.quit();
            }
            if (k == System.Windows.Input.Key.D1)
            {
                ArtDesk.Instance.FadeIn();
            }
            if (k == System.Windows.Input.Key.D2)
            {
                ArtDesk.Instance.FadeOut();
            }
        }

        public void onUpdate()
        {
            
        }
    }
}
