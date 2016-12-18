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
    /// Interaction logic for ControlPanel.xaml
    /// </summary>
    public partial class ControlWindow : Window
    {
        public ControlWindow()
        {
            InitializeComponent();
            this.Show();
        }

        public static ControlWindow Instance
        {
            get
            {
                return Singleton<ControlWindow>.Instance;
            }
        }

        private void mButtonFun_Click(object sender, RoutedEventArgs e)
        {
            ArtDesk.Instance.FadeIn();
        }

        private void mButtonArt_Click(object sender, RoutedEventArgs e)
        {
            ArtDesk.Instance.FadeOut();
        }
    }
}
