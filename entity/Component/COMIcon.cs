using ns_artDesk.core;
using ns_artDesk.view.widget;
using System;

namespace ns_artDesk
{
    class COMIcon : CComponent
    {
        UIIcon mIcon = null;
        public override void onAwake()
        {
            base.onAwake();
            doGetUIBase = () =>
            {
                if (mIcon == null)
                {
                    mIcon = new UIIcon();
                    mIcon.evtDoubleClick += () =>
                    {
                        doEvtDoubleClick?.Invoke();
                    };
                }
                return mIcon;
            };

            doGetUI = doGetUIBase;
        }

        internal Func<UIIcon> doGetUIBase = null;
        internal Func<UIIcon> doGetUI = null;

        public UIIcon getUI()
        {
            mIcon = doGetUI();
            return mIcon;
        }

        public UIIcon currentUI()
        {
            return mIcon;
        }

        internal Action doEvtDoubleClick = null;
        public event Action evtDoubleClick
        {
            add
            {
                doEvtDoubleClick += value;
            }
            remove
            {
                doEvtDoubleClick = null;
            }
        }
    }
}
