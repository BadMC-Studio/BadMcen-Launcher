using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using BadMcen_Launcher.Models.Create;
using Microsoft.UI.Xaml.Media.Imaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BadMcen_Launcher.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            InvokeExternalCode();
        }

        private void InvokeExternalCode()
        {
            CreateOrSearchFiles CreateOrSearchFilesObject = new CreateOrSearchFiles();
            CreateOrSearchFolders CreateOrSearchFoldersObject = new CreateOrSearchFolders();
            string[] files = Directory.GetFiles(Path.Combine(CreateOrSearchFilesObject.FolderPath, @"BadMC\BadMcen Launcher\Wallpaper"), "*.png");
            if (files.Length > 0)
            {
                Random random = new Random();
                string randomFile = files[random.Next(files.Length)];
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(randomFile));
                MainGrid.Background = brush;
            }
        }

    }
}
