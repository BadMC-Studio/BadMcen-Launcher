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
using BadMcen_Launcher.Models.Create;
using Microsoft.UI.Xaml.Media.Imaging;
using BadMcen_Launcher.Views.Home;
using BadMcen_Launcher.Views.SetVersion;
using BadMcen_Launcher.Models.Theme;

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
            CreateOrSearchFiles CreateOrSearchFilesObject = new CreateOrSearchFiles();
            CreateOrSearchFiles CreateOrSearchFoldersObject = new CreateOrSearchFiles();
            
        }

        //Navigate to HomePage or SetVersionPage
        private void AppView(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content == null || MainFrame.Content.GetType() == typeof(SetVersionPage))
            {
                MainFrame.Navigate(typeof(HomePage));
            }
            else if (MainFrame.Content.GetType() == typeof(HomePage))
            {
                MainFrame.Content = null;
            }
        }

        private void SetVersionClick(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content == null || MainFrame.Content.GetType() == typeof(HomePage))
            {
                MainFrame.Navigate(typeof(SetVersionPage));
            }
            else if (MainFrame.Content.GetType() == typeof(SetVersionPage))
            {
                MainFrame.Content = null;
            }
        }

    }

    public partial class MainPage
    {
        public static MainPage Instance { get; private set; }
    }

}
