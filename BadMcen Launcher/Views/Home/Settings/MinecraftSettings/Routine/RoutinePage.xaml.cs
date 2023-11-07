using BadMcen_Launcher.Models.ToastNotifications;
using BadMcen_Launcher.Views.SetVersion;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.VisualBasic;
using MinecraftLaunch.Modules.Models.Launch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using static BadMcen_Launcher.Models.CreateOrUse.CreateOrUseFiles;
using static BadMcen_Launcher.Models.Definition;
using static BadMcen_Launcher.Views.Home.Settings.MinecraftSettings.Routine.RoutinePage;
using static BadMcen_Launcher.Views.Home.Settings.MinecraftSettings.Routine.SetJavaPathPage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BadMcen_Launcher.Views.Home.Settings.MinecraftSettings.Routine
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RoutinePage : Page
    {
        object InitialMinecraftPath = LaunchInfo.ReadJson("MinecraftPath");
        object InitialVersionIsolation = LaunchInfo.ReadJson("VersionIsolation");
        object InitialJavaPath = LaunchInfo.ReadJson("JavaPath");
        object minJVMMemory = LaunchInfo.ReadJson("MinJVMMemory");
        object maxJVMMemory = LaunchInfo.ReadJson("MaxJVMMemory");

        long maxMemory = GC.GetGCMemoryInfo().TotalAvailableMemoryBytes;
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
        public RoutinePage()
        {
            this.InitializeComponent();
            InitialInformation();
            GetUseMemory();
            SetMaxJVMMemory();
            
        }
        //Initial Information
        private void InitialInformation()
        {
            try
            {
                //Initializing SetVersionButton
                if (InitialMinecraftPath != null)
                {
                    //Minecraft Path
                    SetVersionPath.Content = InitialMinecraftPath;
                }
                else
                {
                    //View Text
                    SetVersionPath.Content = LanguageLoader.resourceLoader.GetString("RoutinePage_ExpanderSetButtonContent01"); ;
                }
                //Initializing VersionIsolationToggleSwitch
                if (InitialVersionIsolation != null)
                {
                    var element = (JsonElement)InitialVersionIsolation;
                    VersionIsolationToggleSwitch.IsOn = element.GetBoolean();
                }
                else
                {
                    VersionIsolationToggleSwitch.IsOn = false;
                }

                if (InitialJavaPath != null)
                {
                    //Java Path
                    SetJavaPath.Content = InitialMinecraftPath;
                }
                else
                {
                    //View Text
                    SetJavaPath.Content = LanguageLoader.resourceLoader.GetString("RoutinePage_ExpanderSetButtonContent01"); ;
                }

                if (minJVMMemory != null && maxJVMMemory != null)
                {
                    //JVM Memory
                    JsonElement MinMemoryjsonElement = (JsonElement)minJVMMemory;
                    JsonElement MaxMemoryjsonElement = (JsonElement)maxJVMMemory;
                    if (MinMemoryjsonElement.TryGetDouble(out double number1))
                    {
                        SetJVMMemorySelector.RangeStart = number1;
                    }
                    if (MaxMemoryjsonElement.TryGetDouble(out double number2))
                    {
                        SetJVMMemorySelector.RangeEnd = number2;
                    }
                    if (MinMemoryjsonElement.TryGetDouble(out double number3) && MaxMemoryjsonElement.TryGetDouble(out double number4))
                    {
                        double ViewMCMemory = (Convert.ToDouble(number4) - Convert.ToDouble(number3) / (1024.0 * 1024.0)) * 0.03;
                        MCMemory.Width = ViewMCMemory;
                    } 
                }
                else
                {
                    //Set Value
                    SetJVMMemorySelector.RangeStart = 0;
                    double maxMemoryInMB = maxMemory / (1024.0 * 1024.0);
                    SetJVMMemorySelector.RangeEnd = (int)Math.Round(maxMemoryInMB);
                }
            }
            catch (Exception ex)
            {
                AppToastNotification.AppInfoToast("Error", LanguageLoader.resourceLoader.GetString("Toast_ErrorToast_ReadJsonError"), ex.Message);
            }
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
            if (result == ContentDialogResult.Primary)
            {
                LaunchInfo.WriteJson("MinecraftPath", SetVersionPathPage.Instance.SetVersionPathListView.SelectedItems[0]);
                SetVersionPath.Content = SetVersionPathPage.Instance.SetVersionPathListView.SelectedItems[0];
            }
        }

        //Version Isolation
        private void VersionIsolationToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
                LaunchInfo.WriteJson("VersionIsolation", VersionIsolationToggleSwitch.IsOn);
        }

        //SetJavaPath
        private async void SetJavaPathClick(object sender, RoutedEventArgs e)
        {
            ContentDialog SetJavaPathdialog = new ContentDialog();
            SetJavaPathdialog.XamlRoot = this.XamlRoot;
            //DialogContentStyle
            SetJavaPathdialog.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            //DialogContentTitle
            SetJavaPathdialog.Title = LanguageLoader.resourceLoader.GetString("RoutinePage_ExpanderSetJavaPath01");
            //PrimaryButton
            SetJavaPathdialog.PrimaryButtonText = LanguageLoader.resourceLoader.GetString("RoutinePage_ExpanderSetJavaPath02");
            //CloseButton
            SetJavaPathdialog.CloseButtonText = LanguageLoader.resourceLoader.GetString("RoutinePage_ExpanderSetJavaPath03");
            //DefaultButton
            SetJavaPathdialog.DefaultButton = ContentDialogButton.Primary;
            //DialogContentPage
            SetJavaPathdialog.Content = new SetJavaPathPage(SetJavaPathdialog);
            //PrimaryButtonEnabled
            SetJavaPathdialog.IsPrimaryButtonEnabled = false;
            var result = await SetJavaPathdialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                LaunchInfo.WriteJson("JavaPath", (SetJavaPathPage.Instance.SetJavaPathListView.SelectedItem as SetJavaBind).JavaPath);
                SetJavaPath.Content = (SetJavaPathPage.Instance.SetJavaPathListView.SelectedItem as SetJavaBind).JavaPath;
            }
        }

        //JVM Memory
        private void GetUseMemory()
        {

            foreach (ManagementObject obj in searcher.Get())
            {
                SystemMemory.Width = Convert.ToInt64(obj["TotalVisibleMemorySize"]) - Convert.ToInt64(obj["FreePhysicalMemory"]) / maxMemory / 300;
            }

        }
        private void SetMaxJVMMemory()
        {
            if (maxMemory != -1)
            {
                double maxMemoryInMB = maxMemory / (1024 * 1024);
                RoutineBind routineBind = new RoutineBind { MaxJVMMemory = (int)Math.Round(maxMemoryInMB) };
                DataContext = routineBind;
            }
            else
            {
                RoutineBind routineBind = new RoutineBind { MaxJVMMemory = 16000 };
                DataContext = routineBind;
            }
        }

        private void SetJVMMemory_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            LaunchInfo.WriteJson("MinJVMMemory", SetJVMMemorySelector.RangeStart);
            LaunchInfo.WriteJson("MaxJVMMemory", SetJVMMemorySelector.RangeEnd);
            double ViewMCMemory = (SetJVMMemorySelector.RangeEnd / (1024 * 1024)) * 100 / 300;
            MCMemory.Width = (int)Math.Round(ViewMCMemory);

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
        private void SetJavaPathHelpClick(object sender, RoutedEventArgs e)
        {
            SetJavaPathHelpTeachingTip.IsOpen = true;
        }
        private void SetJVMMemoryHelpClick(object sender, RoutedEventArgs e)
        {
            SetJVMMemoryHelpTeachingTip.IsOpen = true;
        }

        public class RoutineBind
        {
            public double MaxJVMMemory { get; set; }
        }
    }
}
