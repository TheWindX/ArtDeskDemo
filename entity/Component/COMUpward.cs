using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_artDesk
{ 
    [RequireCom(typeof(COMListItem))]
    class COMUpward : CComponent
    {
        public override void onAwake()
        {
            base.onAwake();
            var icon = getComponent<COMIcon>();
            icon.evtDoubleClick += () =>
            {
                ArtBrowser.Instance.exitFrom();
            };
            icon.getUI().title = "";
            icon.getUI().mIcon.Source = IconManager.Instance.upwardIcon;
        }
    }
}
