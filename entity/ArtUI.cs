using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ns_artDesk
{
    abstract class ArtUI : EntityBase
    {
        abstract public FrameworkElement getUI();
        public double x
        {
            get
            {
                return (double)getUI().GetValue(Canvas.LeftProperty);
            }
            set
            {
                getUI().SetValue(Canvas.LeftProperty, value);
            }
        }

        public double y
        {
            get
            {
                return (double)getUI().GetValue(Canvas.TopProperty);
            }
            set
            {
                getUI().SetValue(Canvas.TopProperty, value);
            }
        }

        public double opacity
        {
            get
            {
                return getUI().Opacity;
            }
            set
            {
                getUI().Opacity = value;
            }
        }
    }
}
