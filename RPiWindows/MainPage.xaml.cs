using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using RPiWindows.Controllers;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RPiWindows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MovementController movementController;
        private CameraController cameraController;
        private KeyHandler keyHandler;

        private List<IController> controllers; 

        public MainPage()
        {
            movementController = new MovementController();
            cameraController = new CameraController();
            keyHandler = new KeyHandler(movementController, cameraController);

            controllers = new List<IController>
            { 
                cameraController,
                movementController
            };

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp; 
            this.InitializeComponent();
        }

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            Debug.Write(args.VirtualKey);
            //keyHandler.OnKeyPressUp(args.VirtualKey);
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            Debug.Write(args.VirtualKey);
            
            //keyHandler.OnKeyPressDown(args.VirtualKey);
        }

        private void btnIpAddress_Click(object sender, RoutedEventArgs e)
        {
            // Sanitize input
            string ipAndPort = tbxIpAddress.Text;
            bool isValid = Regex.IsMatch(ipAndPort, @"[0-9]+(?:\.[0-9]+){3}:[0-9]+$");

            if (isValid)
            {
                string ipAddress = ipAndPort.Split(':')[0];
                string port = ipAndPort.Split(':')[1];

                NotifyObserversAddressPortChange(ipAddress, port);
                tbkErrorMsg.Visibility = Visibility.Collapsed;
                tbkIpAddress.Text = ipAndPort;
            }
            else
            {
                tbkErrorMsg.Visibility = Visibility.Visible;
            }
        }

        private void btnCamera_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NotifyObserversAddressPortChange(string ipAddress, string port)
        {
            foreach (var controller in controllers)
            {
                controller.OnDestinationAddressPortChange(ipAddress, port);
            }
        }
    }
}
