using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl.Util;
using MinecraftLaunch;
using MinecraftLaunch.Modules.Enum;
using MinecraftLaunch.Modules.Installer;
using MinecraftLaunch.Modules.Models.Launch;
using MinecraftLaunch.Modules.Utils;

namespace BadMcen_Launcher.Models.MinecraftLaunchCode
{
    class Java
    {
        public static IEnumerable<JavaInfo> SearchJava()
        {

            IEnumerable<JavaInfo> JavaList = JavaUtil.GetJavas();
            if (JavaList != null)
            {
                return JavaList;
            }

            return null;
        }
        public static async Task<string> GetJava(JdkDownloadSource JDKSource, OpenJdkType JDKType, string DownloadPath)
        {
            JavaInstaller installer = new(JDKSource, JDKType, DownloadPath);

            installer.ProgressChanged += (_, x) =>
            {
                Debug.WriteLine(x.Progress);
                Debug.WriteLine(x.ProgressDescription);
            };

            var result = await Task.Run(async () => await installer.InstallAsync());
            if (result.Success)
            {
                Debug.WriteLine("Done!");
                //下载完成后执行的代码块
            }

            return null;
        }
    }
}
