using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BadMcen_Launcher.Models.CreateOrUse;
using BadMcen_Launcher.Views.Home;
using BadMcen_Launcher.Views.SetVersion;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Uno.Extensions;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BadMcen_Launcher.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {
        public MainView()
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
        //Check Launch Error and Waring
        private void LaunchMCInfoClick(object sender, RoutedEventArgs e)
        {

        }

    }

    public partial class MainView
    {
        public static MainView Instance { get; private set; }
    }
}
