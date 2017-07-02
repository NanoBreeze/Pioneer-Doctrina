using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;
using RPiWindows.Models;

namespace RPiWindows.Controllers
{
    class MovementController
    {
        // WASD, for forward, left, back, right. 
        // qezx for foward left, forward right, backward left, and backward right
        const string DRIVE_FORWARD = "W";
        const string DRIVE_FORWARD_LEFT = "q";
        const string DRIVE_FORWARD_RIGHT = "e";
        const string DRIVE_BACKWARD = "S";
        const string DRIVE_BACKWARD_LEFT = "z";
        const string DRIVE_BACKWARD_RIGHT = "x";
        const string TURN_LEFT = "A";
        const string TURN_RIGHT = "D";


        public MovementController()
        {
        }

        public void MonitorForMovement()
        {
            while (true)
            {
                HandleNavigation(
                    MovementFlags.Instance.IsTurningLeft,
                    MovementFlags.Instance.IsTurningRight,
                    MovementFlags.Instance.IsDrivingForward,
                    MovementFlags.Instance.IsDrivingBackward
                );
            }
        }



        private void HandleNavigation(bool isTurningLeft, bool isTurningRight, bool isDrivingForward, bool isDrivingBackward)
        {
            string ipAddress = NetworkInfo.Instance.IpAddress;
            string port = NetworkInfo.Instance.Port;

            if (isDrivingForward && isTurningLeft)
            {
                Network.SendUDP(ipAddress, port, DRIVE_FORWARD_LEFT);
            }
            else if (isDrivingForward && isTurningRight)
            {
                Network.SendUDP(ipAddress, port, DRIVE_FORWARD_RIGHT);
            }
            else if (isDrivingBackward && isTurningLeft)
            {
                Network.SendUDP(ipAddress, port, DRIVE_BACKWARD_LEFT);
            }
            else if (isDrivingBackward && isTurningRight)
            {
                Network.SendUDP(ipAddress, port, DRIVE_BACKWARD_RIGHT);
            }
            else if (isDrivingForward)
            {
                Network.SendUDP(ipAddress, port, DRIVE_FORWARD);
            }
            else if (isDrivingBackward)
            {
                Network.SendUDP(ipAddress, port, DRIVE_BACKWARD);
            }
            else if (isTurningLeft)
            {
                Network.SendUDP(ipAddress, port, TURN_LEFT);
            }
            else if (isTurningRight)
            {
                Network.SendUDP(ipAddress, port, TURN_RIGHT);
            }
        }
    }
}
