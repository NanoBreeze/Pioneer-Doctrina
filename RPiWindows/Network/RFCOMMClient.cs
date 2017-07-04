using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace RPiWindows.Network
{
    class RfcommClient : IClient
    {
        // Unable to send message via Bluetooth from UWP to Raspberry Pi, nor create IPC pipe, so use IPC localhost:port
        // A real Bluetooth client is listening on localhost:port and once it receives our data, it will send the data via Bluetooth to Raspberry Pi 
        public async void Send(string localhost, string port, string message)
        {
            DatagramSocket socket = new DatagramSocket();

            Stream streamOut;
            try
            {
                streamOut = (await socket.GetOutputStreamAsync(new HostName(localhost), port)).AsStreamForWrite();
            }
            catch
            {
                Debug.Write("Something wrong happened. Likely bad port");
                return;
            }
            StreamWriter writer = new StreamWriter(streamOut);
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
            Debug.Write(message);


            //if (socket == null)
            //{
            //socket = new StreamSocket();
            //await socket.ConnectAsync(new HostName(bluetoothAddress), "1", SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication); // Element not found error. Hypothesis is incompatible systems
            //}

            //Stream streamOut = socket.OutputStream.AsStreamForWrite();
            //StreamWriter writer = new StreamWriter(streamOut);
            //await writer.WriteLineAsync(message);
            //await writer.FlushAsync();
        }
    }
}
