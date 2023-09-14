using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;
using static BadMcen_Launcher.Models.Definition;

namespace BadMcen_Launcher.Models.CreateOrUse
{
    class CreateOrUseFiles
    {
        //Get Path
        PathCode pathCode = new PathCode();
        //Create Json
        public void CreateJson()
        {
            string MCPath = Path.Combine(pathCode.AppDataFolderPath, @"BadMC\BadMcen Launcher\Config\Settings\MCPath.json");
            if (!File.Exists(MCPath)) File.Create(MCPath);
        }

        public void DeleteJson()
        {

        }
    }
}
