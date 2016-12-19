using ns_artDesk.core;
using ns_artDesk.view.widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ns_artDesk
{
    class COMPanel : CComponent
    {
        UIFolder mUI = null;
        public override void onAwake()
        {
            base.onAwake();
            doGetUIBase = () =>
            {
                if (mUI == null)
                {
                    mUI = new UIFolder();
                }
                return mUI;
            };

            doGetUI = doGetUIBase;
        }

        internal Func<UIFolder> doGetUIBase = null;
        internal Func<UIFolder> doGetUI = null;
        public UIFolder getUI()
        {
            mUI = doGetUI();
            mUI.mDesk.Children.Clear();//TODO?
            return mUI;
        }

        public void setDesktop()
        {
            ArtFrame.Instance.getBrowserViewContainer().Children.Clear();
            var ui = getUI();
            ui.mDesk.Children.Clear();
            ArtFrame.Instance.getBrowserViewContainer().Children.Add(ui);

            var items = getComponent<COMLister>().items;
            foreach(var item in items)
            {
                var icon = item.getComponent<COMIcon>().getUI();
                if (icon.Parent != null)
                {
                    CLogger.Instance.info("setDesktop", icon.Parent.ToString());
                    (icon.Parent as Panel).Children.Remove(icon);
                }
                icon.lister = getComponent<COMLister>();
                ui.mDesk.Children.Add(icon);
            }
        }
    }
}
