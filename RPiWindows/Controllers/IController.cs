using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPiWindows.Controllers
{
    interface IController
    {
        void OnDestinationAddressPortChange(string ipAddress, string port);
    }
}
