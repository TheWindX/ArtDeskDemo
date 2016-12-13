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

namespace ns_artDesk.core
{
    class CDriver : Singleton<CDriver>
    {
        List<EntityBase> mEntity = new List<EntityBase>();
        public void init()
        {
            ArtFrame.Instance.init();
            ArtDesk.Instance.init();
            var ts = CTimeService.Instance; //for instance construct
            var eh = CEventHub.Instance; //for instance construct
            CTimerManager.Init(() => (UInt32)ts.getTime());
            CRuntime.Instance.init();
            CFileDownloadController.Instance.init();
        }

        public void update()
        {
            ArtFrame.Instance.update();
            ArtDesk.Instance.update();
            CTimeService.Instance.update();
            CTimerManager.tickAll();
            
            CRuntime.Instance.update();
            CFileDownloadController.Instance.update();

        }

        public void exit()
        {
            ArtFrame.Instance.exit();
            ArtDesk.Instance.exit();
            CRuntime.Instance.exit();
            CFileDownloadController.Instance.exit();
        }
    }
}
