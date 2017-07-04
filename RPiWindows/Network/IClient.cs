using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPiWindows.Network
{
    interface IClient
    {
        void Send(string address, string port, string message);
    }
}
