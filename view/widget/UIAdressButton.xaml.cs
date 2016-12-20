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

namespace ns_artDesk
{
    /// <summary>
    /// Interaction logic for UIAdressButton.xaml
    /// </summary>
    public partial class UIAdressButton : UserControl
    {
        public UIAdressButton()
        {
            InitializeComponent();
            mButton.Click += (obj, arg) =>
            {
                evtClick?.Invoke();
            };
        }

        public string title
        {
            get
            {
                return mButton.Content as string;
            }
            set
            {
                mButton.Content = value;
            }
        }

        public event System.Action evtClick;
    }
}
