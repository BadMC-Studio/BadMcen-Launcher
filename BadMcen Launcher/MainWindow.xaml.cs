using Microsoft.UI.Windowing;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using WinRT.Interop;
using static BadMcen_Launcher.Models.Definition;
using BadMcen_Launcher.Models.CreateOrUse;
using BadMcen_Launcher.Views;
using static BadMcen_Launcher.Models.CreateOrUse.CreateOrUseFiles;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BadMcen_Launcher
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private AppWindow m_appWindow;

        public MainWindow()
        {
            Instance = this;
            SetWindow();
            InvokeExternalCode();
            this.InitializeComponent();
            SetWallpaper();
            MainFrame.Navigate(typeof(MainPage));
        }

        //SetWindow
        public void SetWindow()
        {
            m_appWindow = GetAppWindowForCurrentWindow();
            //Set Window Screen Height and Screen Width
            var ScreenHeight = DisplayArea.Primary.WorkArea.Height;
            var ScreenWidth = DisplayArea.Primary.WorkArea.Width;
            //Set the window drag area
            AppWindow.TitleBar.SetDragRectangles(new RectInt32[]{new RectInt32(800, 0, ScreenWidth - 80, 40)});
            //Set the window width and height and the position of the screen.
            m_appWindow.MoveAndResize(new RectInt32(ScreenWidth - ScreenWidth / 2 - 1100 / 2, (int)(ScreenHeight - ScreenHeight / 2 - 650 / 2), 1100, 650));
            //Set TitleBar title
            m_appWindow.Title = "BadMcen Launcher";
        }

        //Invoke external code
        private void InvokeExternalCode()
        {
            //CreateOrSearchFolders.cs
            CreateOrUseFolders CreateOrSearchFoldersObject = new CreateOrUseFolders();
            CreateOrSearchFoldersObject.CreateInitialFolders();
            //CreateOrSearchFiles.cs
            CreateOrUseFiles CreateOrUseFilesObject = new CreateOrUseFiles();
            CreateOrUseFilesObject.CreateJson();
        }

        //Gets the application window object for the current window
        private Microsoft.UI.Windowing.AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return Microsoft.UI.Windowing.AppWindow.GetFromWindowId(myWndId);
        }
        //SetWallpaper
        public void SetWallpaper()
        {
            PathCode pathCode = new PathCode();
            string folders = Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Wallpaper");
            
            string WallPaperMode = "MicaWallpaper";
            switch (WallPaperMode)
            {
                //Set wallpapers as static
                case "StaticWallpaper":
                    string file = Path.Combine(folders, "skyblock3 (1).jpg");
                    if (File.Exists(file))
                    {
                        ImageBrush brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri(file));
                        MainFrame.Background = brush;
                    }
                    break;
                //Set wallpapers as random
                case "RandomWallpaper":
                    string[] files = Directory.GetFiles(folders, "*.png");
                    if (Directory.GetFiles(folders).Length > 0)
                    {
                        Random random = new Random();
                        string randomFile = files[random.Next(files.Length)];
                        ImageBrush brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri(randomFile));
                        MainFrame.Background = brush;
                    }
                    break;
                //Set wallpaper as acrylic
                case "AcrylicWallpaper":

                    MainWindow.Instance.SystemBackdrop = new DesktopAcrylicBackdrop();
                    bool TrySetDesktopAcrylicBackdrop()
                    {
                        if (Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController.IsSupported())
                        {
                            Microsoft.UI.Xaml.Media.DesktopAcrylicBackdrop DesktopAcrylicBackdrop = new Microsoft.UI.Xaml.Media.DesktopAcrylicBackdrop();
                            this.SystemBackdrop = DesktopAcrylicBackdrop;

                            return true; // Succeeded.
                        }

                        return false; // DesktopAcrylic is not supported on this system.
                    }
                    break;
                //Set wallpaper as mica
                case "MicaWallpaper":
                    MainWindow.Instance.SystemBackdrop = new MicaBackdrop();
                    bool TrySetMicaBackdrop(bool useMicaAlt)
                    {
                        if (Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported())
                        {
                            Microsoft.UI.Xaml.Media.MicaBackdrop micaBackdrop = new Microsoft.UI.Xaml.Media.MicaBackdrop();
                            micaBackdrop.Kind = useMicaAlt ? Microsoft.UI.Composition.SystemBackdrops.MicaKind.BaseAlt : Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base;
                            this.SystemBackdrop = micaBackdrop;

                            return true;
                        }

                        return false;
                    }
                    break;
            }

        }
    }

    public partial class MainWindow
    {
        public static MainWindow Instance { get; private set; }
    }
}

