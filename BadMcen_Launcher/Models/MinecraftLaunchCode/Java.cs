
using System.Runtime.InteropServices;
using BadMcen_Launcher.Models.ToastNotifications;
using MinecraftLaunch.Modules.Installer;
using MinecraftLaunch.Modules.Models.Launch;
using MinecraftLaunch.Modules.Utilities;
using static BadMcen_Launcher.Models.Definition;

namespace BadMcen_Launcher.Models.MinecraftLaunchCode
{
    class Java
    {

        public static IEnumerable<JavaInfo> SearchJava()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                IEnumerable<JavaInfo> JavaList = JavaUtil.GetJavas();
                if (JavaList != null)
                {
                    return JavaList;
                }
            }
            else
            {
                AppToastNotification.AppInfoToast("Error", LanguageLoader.resourceLoader.GetString("SystemError01"), LanguageLoader.resourceLoader.GetString("SystemErrorTitle"));
            }
            return null;
        }
        public static async Task GetJava(string DownloadPath, DownloadJavaInfo InstallJavaInfo)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) 
            {
                JavaInstaller installer = new(DownloadPath, InstallJavaInfo);
                //ProgressToast progressToast = new ProgressToast();

                installer.ProgressChanged += (_, x) =>
                {
                    //progressToast.ToastProgrssBar.Value = x.Progress * 100;
                    //progressToast.ToastHubTitle.Text = x.ProgressDescription;
                };
                var result = await Task.Run(async () => await installer.InstallAsync());

                if (result.Success)
                {
                    //下载完成后执行的代码块
                }
            }
            else
            {
                AppToastNotification.AppInfoToast("Error", LanguageLoader.resourceLoader.GetString("SystemError01"), LanguageLoader.resourceLoader.GetString("SystemErrorTitle"));
            }
        }
        
    }
}
