using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk.core
{
    class CDriver : Singleton<CDriver>
    {
        List<EntityBase> mEntity = new List<EntityBase>();
        public void init()
        {
            ArtApp.Instance.init();
            ArtFrame.Instance.init();
            ArtDesk.Instance.init();
            var ts = CTimeService.Instance; //for instance
            CTimerManager.Init(() => (UInt32)ts.getTime());
            CRuntime.Instance.init();
        }

        public void update()
        {
            ArtApp.Instance.update();
            ArtFrame.Instance.update();
            ArtDesk.Instance.update();
            CTimeService.Instance.update();
            CTimerManager.tickAll();
            CRuntime.Instance.update();
        }

        public void exit()
        {
            ArtApp.Instance.exit();
            ArtFrame.Instance.exit();
            ArtDesk.Instance.exit();
            CRuntime.Instance.exit();
        }
    }
}
