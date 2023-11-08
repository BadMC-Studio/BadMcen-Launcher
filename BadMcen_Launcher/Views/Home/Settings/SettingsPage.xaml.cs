using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BadMcen_Launcher.Views.Home.Settings.Routine;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

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
            if (MainPage.Instance.MainFrame.CanGoBack)
            {
                MainPage.Instance.MainFrame.GoBack();

            }
        }
    }
}
