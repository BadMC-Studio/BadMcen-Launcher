using BadMcen_Launcher.Models.CreateOrUse;
using BadMcen_Launcher.Models.MinecraftLaunchCode;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MinecraftLaunch.Modules.Models.Launch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
                await CreateOrUseFiles.SetJavaPathJson.DeleteJsonElement((SetJavaPathListView.SelectedItem as JavaView).JavaPath);//这里除了问题ok
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

            List<JavaView> JavaList = SetJavaPathListView.Items
                .OfType<JavaView>()
                .ToList() ?? new();

            IEnumerable<JavaInfo> javaList = await Task.Run(Java.SearchJava);

            JavaList.AddRange(javaList.Select(x =>
            {
                string name = $"Java{x.JavaVersion} {(x.Is64Bit ? "- 64bit" : "- 32bit")}";
                return new JavaView(name, x.JavaPath);
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
            Trace.WriteLine(SetJavaPathListView.Items.Count);

            //Pop-up message
            SuccessMessage($"{javaList.Count()}{LanguageLoader.resourceLoader.GetString("SetJavaPathPage_InfoMessageSubTitle")}");


        }


        public class JavaView
        {
            public string JavaName { get; set; }
            public string JavaPath { get; set; }

            public JavaView(string name, string path)
            {
                JavaPath = path;
                JavaName = name;
            }
        }
    }
}
