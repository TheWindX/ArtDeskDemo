﻿using Microsoft.Win32;
using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace ns_artDesk
{
    class ArtDesk : ArtUI
    {
        public static ArtDesk Instance
        {
            get
            {
                return Singleton<ArtDesk>.Instance;
            }
        }

        public ArtDesk()
        {
            var w = System.Windows.SystemParameters.WorkArea.Width;
            var h = System.Windows.SystemParameters.WorkArea.Height;
            getUI().Width = w;
            getUI().Height = h;
            x = 0;
            y = 0;
            syncWallPaper();
        }

        public override FrameworkElement getUI()
        {
            return ArtFrame.Instance.getMainWindow().mArtDesk;
        }

        public void syncWallPaper()
        {
            string path = (string)Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop").GetValue("WallPaper");
            CLogger.Instance.info("ArtDesk", "set wallpaper at path {0}", path);
            (getUI() as Panel).Background = new ImageBrush(new BitmapImage(new Uri(path)));
        }

        const int timeToMove = 200;
        ActionMove mMoveIn = new ActionMove(0, 0, timeToMove);
        ActionMove mMoveOut = new ActionMove(0, 0, timeToMove);
        public void MoveIn()
        {
            mMoveIn.mTargetLeft = 0;
            mMoveIn.mTagetTop = 0;
            setAction(mMoveIn);
        }

        public void MoveOut()
        {
            var w = System.Windows.SystemParameters.WorkArea.Width;
            var h = System.Windows.SystemParameters.WorkArea.Height;
            mMoveOut.mTargetLeft = w;
            mMoveOut.mTagetTop = 0;
            setAction(mMoveOut);
        }
    }

    
}