using BadMcen_Launcher.ViewModels.Controls;
using CommunityToolkit.WinUI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BadMcen_Launcher.Controls.AppToast
{
    public sealed partial class ProgressToast : UserControl
    {
        public static DispatcherQueue ProgressDispatcherQueue;

        [Reactive]
        public double Progress { get; set; } = 0;

        [Reactive]
        public string ProgressDescription { get; set; } = "fuck";


        public ProgressToastVM ViewModel { get; private set; }  
        public ProgressToast()
        {
            this.InitializeComponent();
            Instance = this;
            DataContext = ViewModel = new();
            ProgressDispatcherQueue = DispatcherQueue.GetForCurrentThread();
            ToastStoryboard.Children[0].SetValue(DoubleAnimation.FromProperty, ToastTranslation.X + 323);
            ToastStoryboard.Children[0].SetValue(DoubleAnimation.ToProperty, ToastTranslation.X);
            ToastStoryboard.Begin();
            GridShadow.Translation += new Vector3(0, 0, 30);
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

    public partial class ProgressToast
    {
        public static ProgressToast Instance { get; private set; }
    }
}