using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BadMcen_Launcher.Models.Definition;

namespace BadMcen_Launcher.Models.ToastNotifications
{
    internal class SystemToastNotification
    {
        public static void ErrorToast(string Subtitle, string ErrorTitle)
        {
            var e = new Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder();
            e.AddArgument("action", "viewConversation");
            e.AddArgument("conversationId", 9813);
            e.AddText(LanguageLoader.resourceLoader.GetString("Toast_ErrorToastTitle"));
            e.AddText(Subtitle);
            e.AddText(LanguageLoader.resourceLoader.GetString("Toast_ErrorToastSubTitle") + ErrorTitle);
            e.Show();
        }
    }
}
