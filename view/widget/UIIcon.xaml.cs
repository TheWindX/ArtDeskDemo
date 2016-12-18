using Svg2Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ns_artDesk.view.widget
{
    /// <summary>
    /// Interaction logic for UIIcon.xaml
    /// </summary>
    public partial class UIIcon : UserControl
    {
        public UIIcon()
        {
            InitializeComponent();
            choosed = false;
            mIcon.Source = IconManager.Instance.appIcon;
        }
        
        public string title
        {
            get
            {
                return mTitle.Text;
            }
            set
            {
                mTitle.Text = value;
            }
        }

        internal COMLister lister
        {
            get; set;
        }

        bool mChoosed = false;
        Brush enterBrush = new SolidColorBrush(Color.FromArgb(30, 255, 255, 255));
        Brush selectedBrush = new SolidColorBrush(Color.FromArgb(80, 255, 255, 255));
        Brush unselectedBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
        public bool choosed
        {
            get
            {
                return mChoosed;
            }
            set
            {
                mChoosed = value;
                if (value)
                {
                    mFrame.Background = selectedBrush;
                    
                    var uis = lister.currentItems().Select(item => item.getComponent<COMIcon>().currentUI());
                    foreach (var ui in uis)
                    {
                        if (ui != this)
                        {
                            ui.choosed = false;
                        }
                    }
                }
                else
                    mFrame.Background = unselectedBrush;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                evtDoubleClick?.Invoke();
            }
        }

        public event Action evtDoubleClick;

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            if(!choosed)
                mFrame.Background = enterBrush;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!choosed)
                mFrame.Background = unselectedBrush;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            choosed = true;
        }
    }
}
