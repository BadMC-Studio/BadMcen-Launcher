using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Animation;
using System.Threading.Tasks;
using System.Numerics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BadMcen_Launcher.Controls.AppToast
{
    public sealed partial class InfoToast : UserControl
    {
        public InfoToast()
        {
            this.InitializeComponent();
            ToastStoryboard.Children[0].SetValue(DoubleAnimation.FromProperty, ToastTranslation.X + 323);
            ToastStoryboard.Children[0].SetValue(DoubleAnimation.ToProperty, ToastTranslation.X);
            ToastStoryboard.Begin();
            GridShadow.Translation += new Vector3(0, 0, 30);
            TaskCloseToast();
        }
        public async Task TaskCloseToast()
        {
            await Task.Delay(5000);
            ToastStoryboard.Children[0].SetValue(DoubleAnimation.FromProperty, ToastTranslation.X);
            ToastStoryboard.Children[0].SetValue(DoubleAnimation.ToProperty, ToastTranslation.X + 323);
            ToastStoryboard.Begin();
            var animation = ToastStoryboard.Children[0] as DoubleAnimation;
            animation.Completed += (s, e) =>
            {
                MainWindow.Instance.ToastStackPanel.Children.Remove(this);
            };
        }
        private void CloseToastClick(object sender, RoutedEventArgs e)
        {
            ToastStoryboard.Children[0].SetValue(DoubleAnimation.FromProperty, ToastTranslation.X);
            ToastStoryboard.Children[0].SetValue(DoubleAnimation.ToProperty, ToastTranslation.X + 313);
            ToastStoryboard.Begin();
            var animation = ToastStoryboard.Children[0] as DoubleAnimation;
            animation.Completed += (s, e) =>
            {
                MainWindow.Instance.ToastStackPanel.Children.Remove(this);
            };
        }
        public void AppToast(string ToastType, string ToastTitleText, string ToastSubTitleText)
        {
            switch (ToastType)
            {
                case "Error":
                    ToastTitle.Text = ToastTitleText;
                    ToastIcon.Foreground = (SolidColorBrush)Application.Current.Resources["SystemFillColorCriticalBrush"];
                    ToastIcon.Glyph = "\uEB90";
                    break;
                case "Warning":
                    ToastTitle.Text = ToastTitleText;
                    ToastIcon.Foreground = (SolidColorBrush)Application.Current.Resources["SystemFillColorCautionBrush"];
                    ToastIcon.Glyph = "\uF736";
                    break;
                case "Info":
                    ToastTitle.Text = ToastTitleText;
                    ToastIcon.Foreground = (SolidColorBrush)Application.Current.Resources["SystemFillColorAttentionBrush"];
                    ToastIcon.Glyph = "\uF167";
                    break;
                case "Success":
                    ToastTitle.Text = ToastTitleText;
                    ToastIcon.Foreground = (SolidColorBrush)Application.Current.Resources["SystemFillColorSuccessBrush"];
                    ToastIcon.Glyph = "\uEC61";
                    break;
            }
            ToastSubTitle.Text = ToastSubTitleText;
        }
    }
}
