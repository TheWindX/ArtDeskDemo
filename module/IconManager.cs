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
                    var path = "resource/chrome.svg";
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
