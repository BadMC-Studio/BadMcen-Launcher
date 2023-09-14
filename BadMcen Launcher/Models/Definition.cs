using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMcen_Launcher.Models
{
    internal class Definition
    {
        public class PathCode
        {
            //Get AppData path
            public string AppDataFolderPath = Windows.Storage.ApplicationData.Current.RoamingFolder.Path;
        }
        //
        public class LanguageLoader { public static ResourceLoader resourceLoader = new ResourceLoader(); }
    }
}
