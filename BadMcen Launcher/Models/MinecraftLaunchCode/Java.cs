using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinecraftLaunch;
using MinecraftLaunch.Modules.Models.Launch;

namespace BadMcen_Launcher.Models.MinecraftLaunchCode
{
    class Java
    {
        public static IEnumerable<JavaInfo> SearchJava()
        {

            IEnumerable<JavaInfo> JavaList = MinecraftLaunch.Modules.Utils.JavaUtil.GetJavas();
            if (JavaList != null)
            {
                return JavaList;
            }
            return null;
        }
    }
}
