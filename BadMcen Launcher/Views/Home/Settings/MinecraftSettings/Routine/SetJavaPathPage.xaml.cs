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
using static BadMcen_Launcher.Models.Definition;
using Windows.Foundation.Collections;
using BadMcen_Launcher.Models.MinecraftLaunchCode;
using MinecraftLaunch.Modules.Installer;
using MinecraftLaunch.Modules.Models.Launch;
using BadMcen_Launcher.Models.CreateOrUse;
using System.Threading.Tasks;
using System.Diagnostics;
using MinecraftLaunch.Modules.Utils;

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
            setJavaPathdialog = SetJavaPathdialog;
            ListViewIsOrIsNotEmpty();
        }

        private void DownloadJavaClick(object sender, RoutedEventArgs e)
        {

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


        //File picker
        private async void SetJavaPathPicker(object sender, RoutedEventArgs e)
        {
            
        }
        //Delete path
        private async void DeleteJavaPathClick(object sender, RoutedEventArgs e)
        {

            if (SetJavaPathListView.SelectedIndex != -1)
            {
                //Delete from Json
                await CreateOrUseFiles.SetJavaPathJson.DeleteJsonElement(SetJavaPathListView.SelectedItem.ToString());
                //Delete from ListView
                SetJavaPathListView.Items.RemoveAt(SetJavaPathListView.SelectedIndex);
                //Done button becomes disabled
                setJavaPathdialog.IsPrimaryButtonEnabled = false;
                //Text appears
                ListViewIsOrIsNotEmpty();
                //Pop-up message
                SuccessMessage(LanguageLoader.resourceLoader.GetString("SetVersionPathPage_SuccessMessage02"));
            }
            else
            {
                //Pop-up message
                ErrorMessage(LanguageLoader.resourceLoader.GetString("SetVersionPathPage_ErrorMessage03"));
            }
        }

        private void SetJavaPathListViewClick(object sender, ItemClickEventArgs e)
        {
            //Done button becomes Eabled
            setJavaPathdialog.IsPrimaryButtonEnabled = true;
        }


        private async void SearchJavaClick(object sender, RoutedEventArgs e)
        {
            //ReadJson and remove duplicates
            List<string> JavaList = CreateOrUseFiles.SetJavaPathJson.ReadJson();
            IEnumerable<JavaInfo> javaList = Java.SearchJava();
            if (JavaList != null)
            {
                foreach (JavaInfo JavaPath in javaList)
                {
                    JavaList.Add(JavaPath.JavaPath);
                }
                foreach (var javalist in JavaList.Distinct())
                {
                    //JavaPath to JavaInfo
                    JavaInfo JavaPathInfo = JavaUtil.GetJavaInfo(javalist);
                    //Close text
                    ListViewIsOrIsNotEmpty();
                    //Add to ListView
                    SetJavaPathListView.Items.Add(new JavaView() { JavaName = $"Java{JavaPathInfo.JavaVersion} {(JavaPathInfo.Is64Bit ? "- 64bit" : "- 32bit")}", JavaPath = JavaPathInfo.JavaPath });
                    //Write to Json
                    await CreateOrUseFiles.SetJavaPathJson.WriteJson(JavaPathInfo.JavaPath);
                }
                
            }
            else
            {
                List<string> javalist = new List<string>();
                javalist.AddRange(javaList.Select(javaInfo => javaInfo.JavaPath));
                foreach (var javaPathlist in javaList.Distinct())
                {

                    //Close text
                    ListViewIsOrIsNotEmpty();
                    //Add to ListView
                    SetJavaPathListView.Items.Add(new JavaView() { JavaName = $"Java{javaPathlist.JavaVersion} {(javaPathlist.Is64Bit ? "- 64bit" : "- 32bit")}", JavaPath = javaPathlist.JavaPath });
                    //Write to Json
                    await CreateOrUseFiles.SetJavaPathJson.WriteJson(javaPathlist.JavaPath);
                }
                JavaList.Clear();
            }

            //Pop-up message
            SuccessMessage($"{javaList.Count()}{LanguageLoader.resourceLoader.GetString("SetJavaPathPage_InfoMessageSubTitle")}");


        }


        public class JavaView
        {
            public string JavaName { get; set; }
            public string JavaPath { get; set; }
        }
    }
}
