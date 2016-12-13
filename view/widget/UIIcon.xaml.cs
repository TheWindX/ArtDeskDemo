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
            choosed = true;
        }

        public Brush BG
        {
            get
            {
                return mIcon.Background;
            }
            set
            {
                mIcon.Background = value;
            }
        }

        public void setBGIMG(string localUrl)
        {
            BG = new ImageBrush(new BitmapImage(new Uri(localUrl)));
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

        bool mChoosed = false;
        Brush selectedBrush = new SolidColorBrush(Color.FromArgb(30, 0, 0, 255));
        Brush unselectedBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 255));
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
                    mFrame.Background = selectedBrush;
                else
                    mFrame.Background = unselectedBrush;
            }
        }
        
    }
}
