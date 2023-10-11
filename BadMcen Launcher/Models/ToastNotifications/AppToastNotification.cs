using BadMcen_Launcher.Controls.AppToast;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMcen_Launcher.Models.ToastNotifications
{
    internal class AppToastNotification
    {
        public static void ErrorToast(string ToastType, string ToastTitleText, string ToastSubTitleText)
        {
            InfoToast infoToast = new InfoToast();
            MainWindow.Instance.ToastStackPanel.Children.Add(infoToast);
            infoToast.AppToast(ToastType, ToastTitleText, ToastSubTitleText);
        }
    }
}
