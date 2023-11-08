using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

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
                MainPage.Instance.ToastStackPanel.Children.Remove(this);
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
                MainPage.Instance.ToastStackPanel.Children.Remove(this);
            };
        }
        public void AppToast(string ToastType, string ToastTitleText, string ToastSubTitleText)
        {
            ToastTitle.Text = ToastTitleText;
            ToastSubTitle.Text = ToastSubTitleText;
            switch (ToastType)
            {
                case "Error":
                    ToastIcon.Foreground = (SolidColorBrush)Application.Current.Resources["SystemFillColorCriticalBrush"];
                    ToastIcon.Glyph = "\uEB90";
                    break;
                case "Warning":
                    ToastIcon.Foreground = (SolidColorBrush)Application.Current.Resources["SystemFillColorCautionBrush"];
                    ToastIcon.Glyph = "\uF736";
                    break;
                case "Info":
                    ToastIcon.Foreground = (SolidColorBrush)Application.Current.Resources["SystemFillColorAttentionBrush"];
                    ToastIcon.Glyph = "\uF167";
                    break;
                case "Success":
                    ToastIcon.Foreground = (SolidColorBrush)Application.Current.Resources["SystemFillColorSuccessBrush"];
                    ToastIcon.Glyph = "\uEC61";
                    break;
            }
        }
    }
}
