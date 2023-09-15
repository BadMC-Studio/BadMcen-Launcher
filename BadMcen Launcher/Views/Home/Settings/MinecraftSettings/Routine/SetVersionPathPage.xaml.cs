using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static BadMcen_Launcher.Models.Definition;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
using Microsoft.UI.Xaml.Media.Animation;
using static BadMcen_Launcher.Models.CreateOrUse.CreateOrUseFiles;
using System.Text.Json;
using Microsoft.UI.Xaml.Shapes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BadMcen_Launcher.Views.Home.Settings.MinecraftSettings.Routine
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SetVersionPathPage : Page
    {
        private ContentDialog setVersionPathdialog;
        public SetVersionPathPage(ContentDialog SetVersionPathdialog)
        {
            this.InitializeComponent();
            Instance = this;
            setVersionPathdialog = SetVersionPathdialog;
            SetVersionPathJson setVersionPathJson = new SetVersionPathJson();
            setVersionPathJson.ReadJson();
            ListViewIsOrIsNotEmpty();


        }
        private void ListViewIsOrIsNotEmpty()
        {
            if (SetVersionPathListView.Items.Count != 0)
            {
                NullFontIcon.Opacity = 0;
                NullTextBlock.Opacity = 0;
            }
            else
            {
                NullFontIcon.Opacity = 1;
                NullTextBlock.Opacity = 1;
            }
        }

        private void ErrorMessage()
        {
            SetVersionPathMessage.Severity = InfoBarSeverity.Error;
            SetVersionPathMessage.Title = LanguageLoader.resourceLoader.GetString("SetVersionPathPage_ErrorMessageTitle");
            SetVersionPathMessage.IsOpen = true;
        }

        private void SuccessMessage()
        {
            SetVersionPathMessage.Severity = InfoBarSeverity.Success;
            SetVersionPathMessage.Title = LanguageLoader.resourceLoader.GetString("SetVersionPathPage_SuccessMessageTitle");
            SetVersionPathMessage.IsOpen = true;
        }

        
        //File picker
        private async void SetVersionPathPicker(object sender, RoutedEventArgs e)
        {
            // Create a folder picker
            FolderPicker openPicker = new Windows.Storage.Pickers.FolderPicker();
            // Retrieve the window handle (HWND) of the current WinUI 3 window.
            var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hwnd);

            // Set options for your folder picker
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;

            // Open the picker for the user to pick a folder
            StorageFolder folder = await openPicker.PickSingleFolderAsync();
            if (folder?.Path == null)
            {
                Debug.WriteLine("Ò»¸öÐ¡²Êµ°awa");
            }
            else if (SetVersionPathListView.Items.Contains(folder.Path))
            {
                SetVersionPathMessage.Message = LanguageLoader.resourceLoader.GetString("SetVersionPathPage_ErrorMessage01");
                ErrorMessage();
            }
            else if (folder.Name != ".minecraft")
            {
                SetVersionPathMessage.Message = LanguageLoader.resourceLoader.GetString("SetVersionPathPage_ErrorMessage02");
                ErrorMessage();
            }
            else if (folder.Name == ".minecraft")
            {
                SetVersionPathListView.Items.Add(folder.Path);
                SetVersionPathJson setVersionPathJson = new SetVersionPathJson();
                setVersionPathJson.WriteJson(folder.Path);
                ListViewIsOrIsNotEmpty();
                SetVersionPathMessage.Message = LanguageLoader.resourceLoader.GetString("SetVersionPathPage_SuccessMessage01");
                SuccessMessage();
            }
        }
        //Delete path
        private void DeleteMinecraftPathClick(object sender, RoutedEventArgs e)
        {
            if (SetVersionPathListView.SelectedIndex != -1)
            {
                SetVersionPathJson setVersionPathJson = new SetVersionPathJson();
                setVersionPathJson.DeleteJsonElement(SetVersionPathListView.SelectedItems[0].ToString());
                SetVersionPathListView.Items.RemoveAt(SetVersionPathListView.SelectedIndex);
                setVersionPathdialog.IsPrimaryButtonEnabled = false;
                ListViewIsOrIsNotEmpty();
                SetVersionPathMessage.Message = LanguageLoader.resourceLoader.GetString("SetVersionPathPage_SuccessMessage02");
                SuccessMessage();
            }
            else
            {
                SetVersionPathMessage.Message = LanguageLoader.resourceLoader.GetString("SetVersionPathPage_ErrorMessage03");
                ErrorMessage();
            }
        }

        private void SetVersionPathListViewClick(object sender, ItemClickEventArgs e)
        {
            setVersionPathdialog.IsPrimaryButtonEnabled = true;
        }
    }
    public partial class SetVersionPathPage
    {
        public static SetVersionPathPage Instance { get; private set; }
    }
}
