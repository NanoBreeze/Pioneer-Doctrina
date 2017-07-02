using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using RPiWindows.Controllers;
using RPiWindows.Models;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RPiWindows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private KeyHandler keyHandler;

        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            keyHandler = new KeyHandler(this);

            MovementController movementController = new MovementController();
            Task.Factory.StartNew(movementController.MonitorForMovement);

        }

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            keyHandler.OnKeyPressUp(args.VirtualKey);
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            keyHandler.OnKeyPressDown(args.VirtualKey);
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

                NetworkInfo.Instance.IpAddress = ipAddress;
                NetworkInfo.Instance.Port = port;

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

        public void ShowForwardGraphic()
        {
            recForward.Fill = new SolidColorBrush(Colors.Green);
        }

        public void ShowBackwardGraphic()
        {
            recBackward.Fill = new SolidColorBrush(Colors.Green);
        }

        public void ShowRightGraphic()
        {
            recRight.Fill = new SolidColorBrush(Colors.Green);
        }

        public void ShowLeftGraphic()
        {
            recLeft.Fill = new SolidColorBrush(Colors.Green);
        }

        public void ShowStopForwardGraphic()
        {
            recForward.Fill = new SolidColorBrush(Colors.Orange);
        }

        public void ShowStopBackwardGraphic()
        {
            recBackward.Fill = new SolidColorBrush(Colors.Orange);
        }

        public void ShowStopRightGraphic()
        {
            recRight.Fill = new SolidColorBrush(Colors.Orange);
        }

        public void ShowStopLeftGraphic()
        {
            recLeft.Fill = new SolidColorBrush(Colors.Orange);
        }



    }
}
