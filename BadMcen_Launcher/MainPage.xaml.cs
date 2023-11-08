using BadMcen_Launcher.Models.CreateOrUse;
using BadMcen_Launcher.Views;
using BadMcen_Launcher.Views.TitleBar.TaskManager;
using BadMcen_Launcher.Views.TitleBar.Widgets;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Globalization;
using static BadMcen_Launcher.Models.Definition;

namespace BadMcen_Launcher
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            ApplicationLanguages.PrimaryLanguageOverride = "zh-CN";
            this.InitializeComponent();
            Instance = this;
            InvokeExternalCode();
            SetWallpaper();
            MainFrame.Navigate(typeof(MainView));
        }
        private void InvokeExternalCode()
        {
            //CreateOrSearchFolders.cs
            CreateOrUseFolders CreateOrSearchFoldersObject = new CreateOrUseFolders();
            CreateOrSearchFoldersObject.CreateInitialFolders();
            //CreateOrSearchFiles.cs
            CreateOrUseFiles CreateOrUseFilesObject = new CreateOrUseFiles();
            CreateOrUseFilesObject.CreateJson();
        }
        //SetWallpaper
        public void SetWallpaper()
        {
            PathCode pathCode = new PathCode();
            string folders = Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Wallpaper");

            string WallPaperMode = "StaticWallpaper";
            switch (WallPaperMode)
            {
                //Set wallpapers as static
                case "StaticWallpaper":
                    string file = Path.Combine(folders, "无标题 (2).png");
                    if (File.Exists(file))
                    {
                        ImageBrush brush = new ImageBrush();
                        brush.Stretch = Stretch.UniformToFill;
                        brush.ImageSource = new BitmapImage(new Uri(file));
                        MainFrame.Background = brush;
                    }
                    break;
                //Set wallpapers as random
                case "RandomWallpaper":
                    string[] files = Directory.GetFiles(folders, "*.png");
                    if (Directory.GetFiles(folders).Length > 0)
                    {
                        Random random = new Random();
                        string randomFile = files[random.Next(files.Length)];
                        ImageBrush brush = new ImageBrush();
                        brush.Stretch = Stretch.UniformToFill;
                        brush.ImageSource = new BitmapImage(new Uri(randomFile));
                        MainFrame.Background = brush;
                    }
                    break;
            }
        }
        private void TaskManagerClick(object sender, RoutedEventArgs e)
        {
            if (TaskAndWidgetsFrame.Content == null || TaskAndWidgetsFrame.Content.GetType() == typeof(MainWidgets))
            {
                TaskAndWidgetsFrame.Navigate(typeof(MainTaskManager), null, new DrillInNavigationTransitionInfo());
            }
            else if (TaskAndWidgetsFrame.Content.GetType() == typeof(MainTaskManager))
            {
                TaskAndWidgetsFrame.Content = null;
            }
        }
    }
    public partial class MainPage
    {
        public static MainPage? Instance { get; private set; }
    }
}
