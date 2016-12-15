﻿using ns_artDesk.core;
using ns_artDesk.view.widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                mUI.mDesk.Children.Clear();//TODO?
                return mUI;
            };

            doGetUI = doGetUIBase;
        }

        internal Func<UIFolder> doGetUIBase = null;
        internal Func<UIFolder> doGetUI = null;
        public UIFolder getUI()
        {
            return doGetUI();
        }
    }
}
