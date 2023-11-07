using CommunityToolkit.WinUI.Notifications;
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
        public static void InfoToast(string Title, string Subtitle)
        {
            var e = new ToastContentBuilder();
            e.AddArgument("action", "viewConversation");
            e.AddArgument("conversationId", 9813);
            e.AddText(Title);
            e.AddText(Subtitle);
            e.Show();
        }
        
    }
}
