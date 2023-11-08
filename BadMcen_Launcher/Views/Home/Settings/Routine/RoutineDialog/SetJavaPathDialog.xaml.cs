using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static BadMcen_Launcher.Models.CreateOrUse.CreateOrUseFiles;

namespace BadMcen_Launcher.Views.Home.Settings.Routine.RoutineDialog
{
    public sealed partial class SetJavaPathDialog : ContentDialog
    {
        public SetJavaPathDialog()
        {
            Instance = this;
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
    public partial class SetJavaPathDialog
    {
        public static SetJavaPathDialog? Instance { get; private set; }
    }
}
