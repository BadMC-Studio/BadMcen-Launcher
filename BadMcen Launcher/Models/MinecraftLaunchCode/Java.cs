using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Linq;
using BadMcen_Launcher.Controls.AppToast;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using MinecraftLaunch.Modules.Installer;
using MinecraftLaunch.Modules.Models.Launch;
using MinecraftLaunch.Modules.Utilities;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using static BadMcen_Launcher.Controls.AppToast.ProgressToast;
using static BadMcen_Launcher.Views.Home.Settings.MinecraftSettings.Routine.SetJavaPathPage;

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
        public async Task GetJava(string DownloadPath, DownloadJavaInfo InstallJavaInfo)
        {
            JavaInstaller installer = new(DownloadPath, InstallJavaInfo);
            ProgressToast progressToast = new ProgressToast();

            installer.ProgressChanged += (_, x) =>
            {
                progressToast.Progress = x.Progress * 100;
                progressToast.ProgressDescription = x.ProgressDescription;
            };
            var result = await Task.Run(async () => await installer.InstallAsync());

            if (result.Success)
            {
                progressToast.ToastHubTitle.Text = "a text";
                Debug.WriteLine("Done!");
                //下载完成后执行的代码块
            }

        }

    }
}
