/*
 * author: xiaofeng.li
 * mail: 453588006@qq.com
 * desc: 
 * */
using Microsoft.Win32;
using ns_artDesk.core;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.IO;

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

        }

        public override FrameworkElement getUI()
        {
            return ArtFrame.Instance.getMainWindow().mBrowserView;
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
