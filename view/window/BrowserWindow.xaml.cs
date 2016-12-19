using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ns_artDesk
{
    /// <summary>
    /// Interaction logic for BrowserWindow.xaml
    /// </summary>
    public partial class BrowserWindow : Window
    {
        public BrowserWindow()
        {
            InitializeComponent();
            mBackImg.Source = IconManager.Instance.left;
            mForwardImg.Source = IconManager.Instance.right;
            mRefresh.Source = IconManager.Instance.refresh;
            mMenuImg.Source = IconManager.Instance.menu;

            WindowsUtil.syncWallPaper(mBrowserView);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            CDriver.Instance.init();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            CDriver.Instance.update();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CDriver.Instance.exit();
        }

        private void mForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            CEventHub.Instance.forward();
        }

        private void mBackwardBtn_Click(object sender, RoutedEventArgs e)
        {
            CEventHub.Instance.backward();
        }
    }
}
