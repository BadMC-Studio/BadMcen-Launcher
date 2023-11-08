using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BadMcen_Launcher.Models.Definition;

namespace BadMcen_Launcher.Models.CreateOrUse
{
    class CreateOrUseFolders
    {
        PathCode pathCode = new PathCode();
        public void CreateInitialFolders()
        {
            // Combine the base folder with your specific folder....
            string MainFolder = Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher");
            string JsonFolder = Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Config");
            string SettingsFolder = Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Config\Settings");
            string WallpaperFolder = Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Wallpaper");
            // Check if folder exists and if not, create it
            if (!Directory.Exists(MainFolder)) Directory.CreateDirectory(MainFolder);
            if (!Directory.Exists(JsonFolder)) Directory.CreateDirectory(JsonFolder);
            if (!Directory.Exists(SettingsFolder)) Directory.CreateDirectory(SettingsFolder);
            if (!Directory.Exists(WallpaperFolder)) Directory.CreateDirectory(WallpaperFolder);
        }

    }
}
