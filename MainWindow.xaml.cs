using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ns_artDesk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = this.Top = 0;
            this.Topmost = true;
            this.ResizeMode = ResizeMode.NoResize;
            Width = System.Windows.SystemParameters.WorkArea.Width;
            Height = System.Windows.SystemParameters.WorkArea.Height;
            
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(
        int uAction, int uParam, string lpvParam, int fuWinIni);

        //public static ArrayList images;
        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        public enum StyleS_Wallpaper : int
        {
            Tiled, Centered, Stretched
        }

        public void SetWallpaper(string path, StyleS_Wallpaper selected)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            if (selected == StyleS_Wallpaper.Stretched)
            {
                key.SetValue(@"WallpaperStyle", 2.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (selected == StyleS_Wallpaper.Centered)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (selected == StyleS_Wallpaper.Tiled)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 1.ToString());
            }

            SystemParametersInfo(SPI_SETDESKWALLPAPER,
                0,
                path,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }


        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            //加了全屏的桌面
            if(e.Key == Key.D1)
            {
                string path = (string)Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop").GetValue("WallPaper");
                mBG.Background = new ImageBrush(new BitmapImage(new Uri(path)));
                Console.WriteLine(path);
            }
            else if (e.Key == Key.D2)
            {

            }
            else if (e.Key == Key.D3)
            {

            }
        }

        //action, 
    }
}
