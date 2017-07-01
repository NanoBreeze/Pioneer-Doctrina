using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;

namespace RPiWindows
{
    class Network
    {
        public static async void SendUDP(string destIpAddress, string destPort)
        {
            DatagramSocket socket = new DatagramSocket();

            //You can use any port that is not currently in use already on the machine. We will be using two separate and random 
            //ports for the client and server because both the will be running on the same machine.

            //Because we will be running the client and server onkkkkkkkkkkkkk the same machine, we will use localhost as the hostname.
            HostName serverHost = new HostName(destIpAddress);


            //Write a message to the UDP echo server.
            Stream streamOut = (await socket.GetOutputStreamAsync(serverHost, destPort)).AsStreamForWrite();
            StreamWriter writer = new StreamWriter(streamOut);
            string message = "Hello, world!";
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
        }
    }
}
