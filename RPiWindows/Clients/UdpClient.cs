using System;
using System.Diagnostics;
using System.IO;
using Windows.Networking;
using Windows.Networking.Sockets;

namespace RPiWindows.Clients
{
    class UdpClient : IClient
    {
        public async void Send(string destIpAddress, string destPort, string message)
        {
            // Using UDP not for any particular reason, though if going into prod, use TCP
            DatagramSocket socket = new DatagramSocket();
            HostName serverHost;
            try
            {
                serverHost = new HostName(destIpAddress);
            }
            catch
            {
                Debug.Write("Invalid IP Address");
                return;
            }

            Stream streamOut;
            try
            {
                 streamOut = (await socket.GetOutputStreamAsync(serverHost, destPort)).AsStreamForWrite();
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
        }
    }
}
