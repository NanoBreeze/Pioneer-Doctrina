using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RPiWindows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            
            this.InitializeComponent();
        }

        private async void tsCamera_Toggled(object sender, RoutedEventArgs e)
        {
            Network.SendUDP("127.0.0.1", "1337");
            ToggleSwitch tsCamera = sender as ToggleSwitch;
            Debug.Assert(tsCamera != null);

            // Is it possible to define a state for the camera button and simply change its state here instead of going into the implementation details?
            if (tsCamera.IsOn)
            {
                btnCamera.IsEnabled = true;
            }
            else
            {
                btnCamera.IsEnabled = false;
            }
        }
    }
}
