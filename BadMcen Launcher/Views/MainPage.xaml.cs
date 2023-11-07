using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using BadMcen_Launcher.Models.CreateOrUse;
using Microsoft.UI.Xaml.Media.Imaging;
using BadMcen_Launcher.Views.Home;
using BadMcen_Launcher.Views.SetVersion;
using Microsoft.UI.Xaml.Media.Animation;
using System.Threading.Tasks;
using BadMcen_Launcher.Models.ToastNotifications;
using BadMcen_Launcher.Models.MinecraftLaunchCode;
using MinecraftLaunch.Modules.Enum;
using MinecraftLaunch.Modules.Installer;
using System.Diagnostics;
using Microsoft.UI;
using MinecraftLaunch.Modules.Models.Launch;
using MinecraftLaunch.Modules.Utilities;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BadMcen_Launcher.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
            Instance = this;
            InvokeExternalCode();
            
        }

        public void InvokeExternalCode()
        {
            CreateOrUseFiles CreateOrSearchFoldersObject = new CreateOrUseFiles();
            CreateOrUseFiles CreateOrSearchFilesObject = new CreateOrUseFiles();

            
        }

        //Navigate to HomePage or SetVersionPage
        private void AppView(object sender, RoutedEventArgs e)
        {
            SetVersionViewButton.IsChecked = false;
            if (MainFrame.Content == null || MainFrame.Content.GetType() == typeof(SetVersionPage))
            {
                //将AppViewButton的颜色改为AccentFillColorDefaultBrush，将FontIcon的颜色改为ControlFillColorDefaultBrush，并加上动画
                MainFrame.Navigate(typeof(HomePage), null);

            }
            else if (MainFrame.Content.GetType() == typeof(HomePage))
            {
                //将AppViewButton的颜色改为Transparent，将FontIcon的颜色改为TextFillColorPrimaryBrush
                MainFrame.Content = null;
            }
        }

        private void SetVersionClick(object sender, RoutedEventArgs e)
        {
            AppViewButton.IsChecked = false;
            if (MainFrame.Content == null || MainFrame.Content.GetType() == typeof(HomePage))
            {
               
                MainFrame.Navigate(typeof(SetVersionPage), null);
            }
            else if (MainFrame.Content.GetType() == typeof(SetVersionPage))
            {
                
                MainFrame.Content = null;
            }
        }

        private void LaunchMCInfoClick(object sender, RoutedEventArgs e)
        {

        }
        
    }

    public partial class MainPage
    {
        public static MainPage Instance { get; private set; }
    }

}
