using BadMcen_Launcher.Models.Create;
using BadMcen_Launcher.Models.Theme;
using BadMcen_Launcher.Views;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
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
using Microsoft.UI.Xaml.Media.Imaging;
using static BadMcen_Launcher.Models.Definition;

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
            InvokeExternalCode();
            SetWindow();
            this.InitializeComponent();
            SetWallpaper();
            Instance = this;
            MainFrame.Navigate(typeof(MainPage));
        }

        //SetWindow
        public void SetWindow()
        {
            m_appWindow = GetAppWindowForCurrentWindow();
            var ScreenHeight = DisplayArea.Primary.WorkArea.Height;
            var ScreenWidth = DisplayArea.Primary.WorkArea.Width;
            //Set the window width and height and the position of the screen.
            m_appWindow.MoveAndResize(new RectInt32(ScreenWidth - ScreenWidth / 2 - 1100 / 2, (int)(ScreenHeight - ScreenHeight / 2 - 650 / 2), 1100, 650));
            //Set TitleBar title
            m_appWindow.Title = "BadMcen Launcher";
            //Set the window drag area
            AppWindow.TitleBar.SetDragRectangles(new RectInt32[] { new RectInt32(80, 0, ScreenWidth - 80, 40) });
        }
        //SetWallpaper
        public void SetWallpaper()
        {
            1
            PathCode pathCode = new PathCode();
            string folders = System.IO.Path.Combine(pathCode.FolderPath, @"BadMC\BadMcen Launcher\Wallpaper");
            string[] files = Directory.GetFiles(folders, "*.png");
            if (Directory.GetFiles(folders).Length > 0)
            {
                Random random = new Random();
                string randomFile = files[random.Next(files.Length)];
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(randomFile));
                MainFrame.Background = brush;
            }
        }

        //Invoke external code
        private void InvokeExternalCode()
        {
            //CreateOrSearchFiles.cs
            CreateOrSearchFiles CreateOrSearchFilesObject = new CreateOrSearchFiles();
            //CreateOrSearchFolders.cs
            CreateOrSearchFolders CreateOrSearchFoldersObject = new CreateOrSearchFolders();
            CreateOrSearchFoldersObject.CreateInitialFolders();
            //Wallpaper.cs

        }

        //Gets the application window object for the current window
        private Microsoft.UI.Windowing.AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return Microsoft.UI.Windowing.AppWindow.GetFromWindowId(myWndId);
        }

    }

    public partial class MainWindow
    {
        public static MainWindow Instance { get; private set; }
    }
}
