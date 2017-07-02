using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPiWindows.Controllers
{
    class CameraController : IController
    {
        private string ipAddress;
        private string port;

        public void CapturePicture()
        {
            
        }

        public void OnDestinationAddressPortChange(string ipAddress, string port)
        {
            this.ipAddress = ipAddress;
            this.port = port;
        }
    }
}
