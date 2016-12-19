using ns_artDesk.core;
using Svg2Xaml;
using System.IO;
using System.Windows.Media;

namespace ns_artDesk
{
    class IconManager : CModule
    {
        public static IconManager Instance
        {
            get
            {
                return Singleton<IconManager>.Instance;
            }
        }

        ImageSource mFolderIcon = null;
        public ImageSource folderIcon
        {
            get
            {
                if(mFolderIcon == null)
                {
                    var path = "resource/folder.svg";
                    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        mFolderIcon = SvgReader.Load(stream);
                    }
                }
                return mFolderIcon;
            }
        }

        ImageSource mAppIcon = null;
        public ImageSource appIcon
        {
            get
            {
                if (mAppIcon == null)
                {
                    var path = "resource/app.svg";
                    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        mAppIcon = SvgReader.Load(stream);
                    }
                }
                return mAppIcon;
            }
        }

        ImageSource mUpwardIcon = null;
        public ImageSource upwardIcon
        {
            get
            {
                if (mUpwardIcon == null)
                {
                    var path = "resource/upward.svg";
                    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        mUpwardIcon = SvgReader.Load(stream);
                    }
                }
                return mUpwardIcon;
            }
        }

        ImageSource mLeft = null;
        public ImageSource left
        {
            get
            {
                if (mLeft == null)
                {
                    var path = "resource/left.svg";
                    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        mLeft = SvgReader.Load(stream);
                    }
                }
                return mLeft;
            }
        }

        ImageSource mRight = null;
        public ImageSource right
        {
            get
            {
                if (mRight == null)
                {
                    var path = "resource/right.svg";
                    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        mRight = SvgReader.Load(stream);
                    }
                }
                return mRight;
            }
        }

        ImageSource mRefresh = null;
        public ImageSource refresh
        {
            get
            {
                if (mRefresh == null)
                {
                    var path = "resource/refresh.svg";
                    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        mRefresh = SvgReader.Load(stream);
                    }
                }
                return mRefresh;
            }
        }

        ImageSource mMenu = null;
        public ImageSource menu
        {
            get
            {
                if (mMenu == null)
                {
                    var path = "resource/menu.svg";
                    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        mMenu = SvgReader.Load(stream);
                    }
                }
                return mMenu;
            }
        }
        //TODO, load icon as img source, from remote


        public void onExit()
        {
            
        }

        public void onInit()
        {
            
        }

        public void onUpdate()
        {
            
        }
    }
}
