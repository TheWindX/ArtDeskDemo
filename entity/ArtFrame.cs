using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace ns_artDesk
{
    class ArtFrame : ArtUI
    {
        public static ArtFrame Instance
        {
            get
            {
                return Singleton<ArtFrame>.Instance;
            }
        }

        public ArtFrame()
        {
            var cw = getControlWindow();
        }

        public override FrameworkElement getUI()
        {
            return getMainWindow();
        }
        public MainWindow getMainWindow()
        {
            return (ArtApp.Instance.getApp().MainWindow) as MainWindow;
        }

        public ControlWindow getControlWindow()
        {
            return ControlWindow.Instance;
        }
    }
}
