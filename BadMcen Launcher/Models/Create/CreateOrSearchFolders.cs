using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMcen_Launcher.Models.Create
{
    class CreateOrSearchFolders
    {
        //Get path
        public string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public void CreateInitialFolders()
        {
            // Combine the base folder with your specific folder....
            string MainFolder = Path.Combine(FolderPath, @"BadMC\BadMcen Launcher");
            string JsonFolder = Path.Combine(FolderPath, @"BadMC\BadMcen Launcher\Config");
            string SettingsFolder = Path.Combine(FolderPath, @"BadMC\BadMcen Launcher\Config\Settings");
            string WallpaperFolder = Path.Combine(FolderPath, @"BadMC\BadMcen Launcher\Wallpaper");
            // Check if folder exists and if not, create it
            if (!Directory.Exists(MainFolder)) Directory.CreateDirectory(MainFolder);
            if (!Directory.Exists(JsonFolder)) Directory.CreateDirectory(JsonFolder);
            if (!Directory.Exists(SettingsFolder)) Directory.CreateDirectory(SettingsFolder);
            if (!Directory.Exists(WallpaperFolder)) Directory.CreateDirectory(WallpaperFolder);
        }

    }
}
