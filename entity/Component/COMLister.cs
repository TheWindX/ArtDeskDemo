using ns_artDesk.core;
using Svg2Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace ns_artDesk
{
    [RequireCom(typeof(COMListItem))]
    [RequireCom(typeof(COMPanel))]
    class COMLister : CComponent
    {
        internal Func<ListEx<COMListItem>> doGetItem = null;

        public ListEx<COMListItem> items
        {
            get
            {
                return doGetItem?.Invoke();
            }
        }

        ListEx<COMListItem> mItem = null;
        public override void onAwake()
        {
            doGetItem = () =>
            {
                if(mItem == null)
                {
                    mItem = new ListEx<COMListItem>();
                }
                return mItem;
            };

            var ui = getComponent<COMIcon>().getUI().mIcon;
            var img = new ImageBrush();
            var path = "resource/Folder.svg";
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                ui.Source = SvgReader.Load(stream);
            }

            getComponent<COMIcon>().getUI().evtDoubleClick += () =>
             {
                 getComponent<COMPanel>().setDesktop();
             };
        }
    }
}
