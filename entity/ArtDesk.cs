/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 
 * */
using Microsoft.Win32;
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
using System.Diagnostics;
using System.IO;
using ns_artDesk.view.widget;

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
            getUI().Width = SystemParameters.PrimaryScreenWidth;
            getUI().Height = SystemParameters.PrimaryScreenHeight;
            Trace.WriteLine("FullPrimaryScreenHeight: " + SystemParameters.FullPrimaryScreenHeight);
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
            if(File.Exists(path) )
            {
                (getUI() as UIDesk).Background = new ImageBrush(new BitmapImage(new Uri(path)));
            }
            else
            {
                (getUI() as UIDesk).Background = new SolidColorBrush(Colors.Black);
            }
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
            var w = SystemParameters.WorkArea.Width;
            var h = SystemParameters.WorkArea.Height;
            mMoveOut.mTargetLeft = w;
            mMoveOut.mTagetTop = 0;
            setAction(mMoveOut);
        }

        const int timeToFade = 1200;
        ActionFade mFadeIn = new ActionFade(0, timeToFade);
        ActionFade mFadeOut = new ActionFade(1, timeToFade);
        public void FadeIn()
        {   
            setAction(mFadeIn);
        }
        public void FadeOut()
        {
            setAction(mFadeOut);
        }
    }

    
}
