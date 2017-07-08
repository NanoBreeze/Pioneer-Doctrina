﻿using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using RPiWindows.Controllers;
using RPiWindows.Models;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using RPiWindows.Clients;
using RPiWindows.Servers;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RPiWindows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private KeyHandler keyHandler;
        private CameraServer cameraServer;

        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            keyHandler = new KeyHandler(this);
            cameraServer = new CameraServer();

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

        private async void btnCamera_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Inside btnCamera_Click");
            StreamSocketListener listener = new StreamSocketListener();

            listener.ConnectionReceived += Listener_ConnectionReceived;
            await listener.BindEndpointAsync(new HostName("10.0.0.96"), "8000");

        }

        private async void Listener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            using (IInputStream inStream = args.Socket.InputStream)
            {
                await cameraServer.HandleInputStream(inStream);
            }
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

        private bool IsValidIpAndPort(string ipAndPort)
        {
            bool isValid = Regex.IsMatch(ipAndPort, @"[0-9]+(?:\.[0-9]+){3}:[0-9]+$");

            return isValid;
        }

        private void btnUseTCP_Click(object sender, RoutedEventArgs e)
        {
            string ipAndPort = tbxIpAddress.Text;
            if (IsValidIpAndPort(ipAndPort))
            {
                string ipAddress = ipAndPort.Split(':')[0];
                string port = ipAndPort.Split(':')[1];

                NetworkModel.Instance.IpAddress = ipAddress;
                NetworkModel.Instance.Port = port;
                NetworkModel.Instance.NetworkClient = new UdpClient(); // Why create new instance each time? Can optimize to not create new instance

                btnUseTCP.Background = new SolidColorBrush(Colors.Green);
                btnUseRFCOMM.Background = new SolidColorBrush(Colors.Orange);

                tbkErrorMsg.Visibility = Visibility.Collapsed;
                tbkIpAddress.Text = ipAndPort;
            }
            else
            {
                tbkErrorMsg.Visibility = Visibility.Visible;
            }
        }

        private void btnUseRFCOMM_Click(object sender, RoutedEventArgs e)
        {
            btnUseRFCOMM.Background = new SolidColorBrush(Colors.Green);
            btnUseTCP.Background = new SolidColorBrush(Colors.Orange);

            NetworkModel.Instance.IpAddress = "127.0.0.1";
            NetworkModel.Instance.Port = "10001";
            NetworkModel.Instance.NetworkClient = new RfcommClient(); // Why create new instance each time? Can optimize to not create new instance
        }
    }
}
