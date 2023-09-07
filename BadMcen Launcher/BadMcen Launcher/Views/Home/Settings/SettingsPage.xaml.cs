using BadMcen_Launcher.Views.Home.Settings.MinecraftSettings.Routine;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BadMcen_Launcher.Views.Home.Settings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }
        //Navigate
        private void SettingsView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = (NavigationViewItem)args.SelectedItem;
            //Navigate to RoutinePage
            if ((string)selectedItem.Tag == "RoutineTag") SettingsFrame.Navigate(typeof(RoutinePage));
            //if ((string)selectedItem.Tag == "Card") RightColumnFrame.Navigate(typeof(CardPage));
        }
        //Return to the previous page
        private void MainPageView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (MainWindow.Instance.MainFrame.CanGoBack)
            {
                MainWindow.Instance.MainFrame.GoBack();

            }
        }

    }
}
