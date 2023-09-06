using BadMcen_Launcher.Models.Create;
using BadMcen_Launcher.Views;
using Microsoft.UI;
using Microsoft.UI.Windowing;
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
            this.InitializeComponent();
            SetWindow();
            InvokeExternalCode();
            MainFrame.Navigate(typeof(MainPage));
        }
        //Invoke external code
        private void InvokeExternalCode()
        {
            CreateOrSearchFiles CreateOrSearchFilesObject = new CreateOrSearchFiles();
            CreateOrSearchFolders CreateOrSearchFoldersObject = new CreateOrSearchFolders();
            CreateOrSearchFoldersObject.CreateInitialFolders();
            string[] files = Directory.GetFiles(CreateOrSearchFilesObject.FolderPath, "*.png");
            
        }
        
        public void SetWindow()
        {
            m_appWindow = GetAppWindowForCurrentWindow();
            var ScreenHeight = DisplayArea.Primary.WorkArea.Height;
            var ScreenWidth = DisplayArea.Primary.WorkArea.Width;
            m_appWindow.MoveAndResize(new RectInt32(ScreenWidth - ScreenWidth / 2 - 1100 / 2, (int)(ScreenHeight - ScreenHeight / 2 - 650 / 2), 1100, 650));//Window width and position
            m_appWindow.Title = "BadMcen Launcher";
            AppWindow.TitleBar.SetDragRectangles(new RectInt32[] { new RectInt32(80, 0, ScreenWidth - 80, 40) });
        }

        private Microsoft.UI.Windowing.AppWindow GetAppWindowForCurrentWindow()
        {

            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return Microsoft.UI.Windowing.AppWindow.GetFromWindowId(myWndId);
        }

    }
}
