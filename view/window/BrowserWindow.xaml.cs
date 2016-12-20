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

            CEventHub.Instance.evtURLChanged += setURL;
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

        private void mMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            var cm = new ContextMenu();
            var mi = new MenuItem();
            cm.Items.Add(mi);
            mi.Header = "控制台";
            mi.Click += (ui, arg) =>
            {
                CSRepl.Instance.Toggle();
            };
            cm.IsOpen = true;
        }

        internal void setURL(List<COMLister> ls)
        {
            mAdressBox.Children.Clear();
            for(int i = 0; i<ls.Count; ++i)
            {
                var item = ls[i];
                var b = new UIAdressButton();
                var url = ls.Take(i+1).ToList();
                b.evtClick += ()=>
                {
                    ArtBrowser.Instance.setAddress(url);
                };
                b.title = item.getComponent<COMIcon>().currentUI().title;
                mAdressBox.Children.Add(b);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CEventHub.Instance.keydown(e.Key);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            CEventHub.Instance.keyup(e.Key);
        }
    }
}
