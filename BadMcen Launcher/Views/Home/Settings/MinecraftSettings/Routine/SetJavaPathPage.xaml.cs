using BadMcen_Launcher.Models.CreateOrUse;
using BadMcen_Launcher.Models.MinecraftLaunchCode;
using BadMcen_Launcher.Models.ToastNotifications;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using MinecraftLaunch.Modules.Enum;
using MinecraftLaunch.Modules.Installer;
using MinecraftLaunch.Modules.Models.Launch;
using MinecraftLaunch.Modules.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;
using static BadMcen_Launcher.Models.CreateOrUse.CreateOrUseFiles;
using static BadMcen_Launcher.Models.Definition;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BadMcen_Launcher.Views.Home.Settings.MinecraftSettings.Routine
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SetJavaPathPage : Page
    {
        private ContentDialog setJavaPathdialog;
        public SetJavaPathPage(ContentDialog SetJavaPathdialog)
        {
            this.InitializeComponent();
            Instance = this;
            setJavaPathdialog = SetJavaPathdialog;
            ReadList();
            ListViewIsOrIsNotEmpty();

        }
        private void ReadList()
        {
            if (SetJavaPathJson.ReadJson() != null)
            {
                foreach (string item in SetJavaPathJson.ReadJson())
                {

                    JavaInfo javaInfo = JavaUtil.GetJavaInfo(item);
                    SetJavaPathListView.Items.Add(new SetJavaBind($"Java{javaInfo.JavaVersion} {(javaInfo.Is64Bit ? "- 64bit" : "- 32bit")}", item));

                }
            }

        }
        private void ListViewIsOrIsNotEmpty()
        {
            if (SetJavaPathListView.Items.Count != 0)
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
        private void ErrorMessage(string MessageTitle)
        {
            SetJavaPathMessage.Severity = InfoBarSeverity.Error;
            SetJavaPathMessage.Title = LanguageLoader.resourceLoader.GetString("ErrorMessageTitle");
            SetJavaPathMessage.IsOpen = true;
            SetJavaPathMessage.Message = MessageTitle;
        }

        private void SuccessMessage(string MessageTitle)
        {
            SetJavaPathMessage.Severity = InfoBarSeverity.Success;
            SetJavaPathMessage.Title = LanguageLoader.resourceLoader.GetString("SuccessMessageTitle");
            SetJavaPathMessage.IsOpen = true;
            SetJavaPathMessage.Message = MessageTitle;
        }

        private async void GetJavaClick(object sender, RoutedEventArgs e)
        {
            if (GetJavaComboBox.SelectedIndex != -1)
            {
                // Create a folder picker
                FolderPicker openPicker = new Windows.Storage.Pickers.FolderPicker();
                // Retrieve the window handle (HWND) of the current WinUI 3 window.
                var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
                WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hwnd);

                // Set options for your folder picker
                openPicker.SuggestedStartLocation = PickerLocationId.Desktop;

                // Open the picker for the user to pick a folder
                StorageFolder ReturnFolder = await openPicker.PickSingleFolderAsync();
                if (ReturnFolder != null)
                {
                    Java java = new Java();
                    AppToastNotification.AppProgrssToast("Info", LanguageLoader.resourceLoader.GetString("SetJavaPathPage_InfoMessage01"), GetJavaComboBox.SelectedItem.ToString());
                    switch (GetJavaComboBox.SelectedItem)
                    {
                        case "OpenJDK 8":
                            await java.GetJava(ReturnFolder.Path, (await JavaInstaller.GetJavasByVersionAsync(JavaVersion.Java8).AsListAsync()).First(x => x.OsType.Contains("64")));
                            break;
                        case "OpenJDK 16":
                            await java.GetJava(ReturnFolder.Path, (await JavaInstaller.GetJavasByVersionAsync(JavaVersion.Java16).AsListAsync()).First(x => x.OsType.Contains("64")));
                            break;
                        case "OpenJDK 17":
                            await java.GetJava(ReturnFolder.Path, (await JavaInstaller.GetJavasByVersionAsync(JavaVersion.Java17).AsListAsync()).First(x => x.OsType.Contains("64")));
                            break;
                    }
                }
            }
            else
            {
                ErrorMessage(LanguageLoader.resourceLoader.GetString("SetJavaPathPage_ErrorMessage06"));
            }
            
        }
        //File picker
        private async void SetJavaPathPicker(object sender, RoutedEventArgs e)
        {
            // Create a file picker
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            // Retrieve the window handle (HWND) of the current WinUI 3 window.
            var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
            // Initialize the file picker with the window handle (HWND).
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hwnd);

            // Set options for your file picker
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.FileTypeFilter.Add(".exe");


            // Open the picker for the user to pick a file
            var ReturnFile = await openPicker.PickSingleFileAsync();
            if (ReturnFile?.Name == "javaw.exe")
            {
                if (SetJavaPathListView.Items.Any(Rf => (Rf as SetJavaBind).JavaPath.Contains(ReturnFile.Path)))
                {
                    ErrorMessage(LanguageLoader.resourceLoader.GetString("SetJavaPathPage_ErrorMessage05"));
                }
                else
                {
                    JavaInfo javaInfo = JavaUtil.GetJavaInfo(ReturnFile.Path);
                    SetJavaPathListView.Items.Add(new SetJavaBind($"Java{javaInfo.JavaVersion} {(javaInfo.Is64Bit ? "- 64bit" : "- 32bit")}", ReturnFile.Path));
                    //Close text
                    ListViewIsOrIsNotEmpty();
                    await SetJavaPathJson.WriteJson(ReturnFile.Path);
                }

            }
            else if (ReturnFile != null && ReturnFile?.Name != "javaw.exe")
            {
                ErrorMessage(LanguageLoader.resourceLoader.GetString("SetJavaPathPage_ErrorMessage04"));
            }

        }
        //Delete path
        private async void DeleteJavaPathClick(object sender, RoutedEventArgs e)
        {

            if (SetJavaPathListView.SelectedIndex != -1)
            {
                //Delete from Json
                await CreateOrUseFiles.SetJavaPathJson.DeleteJsonElement((SetJavaPathListView.SelectedItem as SetJavaBind).JavaPath);
                //Delete from ListView
                SetJavaPathListView.Items.RemoveAt(SetJavaPathListView.SelectedIndex);
                //Done button becomes disabled
                setJavaPathdialog.IsPrimaryButtonEnabled = false;
                //Text appears
                ListViewIsOrIsNotEmpty();
                //Pop-up message
                SuccessMessage(LanguageLoader.resourceLoader.GetString("SetJavaPathPage_SuccessMessage02"));
            }
            else
            {
                //Pop-up message
                ErrorMessage(LanguageLoader.resourceLoader.GetString("SetJavaPathPage_ErrorMessage03"));
            }
        }

        private void SetJavaPathListViewClick(object sender, ItemClickEventArgs e)
        {
            //Done button becomes Eabled
            setJavaPathdialog.IsPrimaryButtonEnabled = true;
        }

        //By Xilu
        private async void SearchJavaClick(object sender, RoutedEventArgs e)
        {
            //ReadJson and remove duplicates
            List<SetJavaBind> JavaList = SetJavaPathListView.Items
                .OfType<SetJavaBind>()
                .ToList() ?? new();

            IEnumerable<JavaInfo> javaList = await Task.Run(Java.SearchJava);

            JavaList.AddRange(javaList.Select(x =>
            {
                string name = $"Java{x.JavaVersion} {(x.Is64Bit ? "- 64bit" : "- 32bit")}";
                return new SetJavaBind(name, x.JavaPath);
            }));

            //分组去重法
            JavaList = JavaList.GroupBy(x => x.JavaPath)
                .Select(x => x.First())
                .ToList();

            SetJavaPathListView.Items.Clear();
            foreach (var javalist in JavaList)
            {

                //Close text
                ListViewIsOrIsNotEmpty();
                //Add to ListView
                SetJavaPathListView.Items.Add(javalist);
                //Write to Json
                await CreateOrUseFiles.SetJavaPathJson.WriteJson(javalist.JavaPath);
            }

            //Pop-up message
            SuccessMessage($"{javaList.Count()}{LanguageLoader.resourceLoader.GetString("SetJavaPathPage_InfoMessageSubTitle")}");


        }

        public class SetJavaBind
        {
            public string JavaName { get; set; }
            public string JavaPath { get; set; }

            public SetJavaBind(string name, string path)
            {
                JavaPath = path;
                JavaName = name;
            }
        }

        
    }
    public partial class SetJavaPathPage
    {
        public static SetJavaPathPage Instance { get; private set; }
    }
}
