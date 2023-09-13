using BadMcen_Launcher.Views.SetVersion;
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
using static BadMcen_Launcher.Models.Definition;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BadMcen_Launcher.Views.Home.Settings.MinecraftSettings.Routine
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RoutinePage : Page
    {
        public RoutinePage()
        {
            this.InitializeComponent();
        }

        //Set Version Path Dialog
        private async void SetVersionPathClick(object sender, RoutedEventArgs e)
        {
            ContentDialog SetVersionPathdialog = new ContentDialog();

            SetVersionPathdialog.XamlRoot = this.XamlRoot;
            //DialogContentStyle
            SetVersionPathdialog.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            //DialogContentTitle
            SetVersionPathdialog.Title = LanguageLoader.resourceLoader.GetString("RoutinePage_ExpanderSetVersionPath01");
            //PrimaryButton
            SetVersionPathdialog.PrimaryButtonText = LanguageLoader.resourceLoader.GetString("RoutinePage_ExpanderSetVersionPath02");
            //CloseButton
            SetVersionPathdialog.CloseButtonText = LanguageLoader.resourceLoader.GetString("RoutinePage_ExpanderSetVersionPath03");
            //DefaultButton
            SetVersionPathdialog.DefaultButton = ContentDialogButton.Primary;
            //DialogContentPage
            SetVersionPathdialog.Content = new SetVersionPathPage(SetVersionPathdialog);
            //PrimaryButtonEnabled
            SetVersionPathdialog.IsPrimaryButtonEnabled = false;
            var result = await SetVersionPathdialog.ShowAsync();
        }

        //Set version path Help
        private void SetVersionPathHelpClick(object sender, RoutedEventArgs e)
        {
            SetVersionPathHelp.IsOpen = true;
        }
        //Isolation of versions info
        private void VersionIsolationHelpClick(object sender, RoutedEventArgs e)
        {
            VersionIsolationHelpTeachingTip.IsOpen = true;
        }
        //Set java path info
        private void InfoClick2(object sender, RoutedEventArgs e)
        {
            RoutineInfo2.IsOpen = true;
        }

    }
}
