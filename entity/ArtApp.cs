using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ns_artDesk
{
    class ArtApp : EntityBase
    {
        public static ArtApp Instance
        {
            get
            {
                return Singleton<ArtApp>.Instance;
            }
        }

        public Application getApp()
        {
            return Application.Current;
        }

        public void quit()
        {
            getApp().Shutdown();
        }
    }
}
