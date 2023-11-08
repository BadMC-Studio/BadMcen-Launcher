using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace BadMcen_Launcher.Models
{
    internal class Definition
    {
        public class PathCode
        {
            //Get AppData path
            public string AppDataFolderPath = ApplicationData.Current.RoamingFolder.Path;
        }
        
        public class LanguageLoader { public static ResourceLoader resourceLoader = new ResourceLoader(); }
    }
}
