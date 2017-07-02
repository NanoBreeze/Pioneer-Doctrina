using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;

namespace RPiWindows.Controllers
{
    class MovementController : IController 
    {
        private string ipAddress;
        private string port;

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

        public void HandleNavigation(bool isTurningLeft, bool isTurningRight, bool isDrivingForward,
            bool isDrivingBackward)
        {
            if (isDrivingForward && isTurningLeft)
            {
                DriveForwardLeft();
            }
            else if (isDrivingForward && isTurningRight)
            {
                DriveForwardRight();
            }
            else if (isDrivingBackward && isTurningLeft)
            {
                DriveBackwardLeft();
            }
            else if (isDrivingBackward && isTurningRight)
            {
                DriveBackwardRight();
            }
            else if (isDrivingForward)
            {
                DriveForward();
            }
            else if (isDrivingBackward)
            {
                DriveBackward();
            }
            else if (isTurningLeft)
            {
                TurnLeft();
            }
            else
            {
                TurnRight();
            }
        }

        private void DriveForward()
        {
            Network.SendUDP(ipAddress, port, DRIVE_FORWARD);
        }

        private void DriveBackward()
        {
            Network.SendUDP(ipAddress, port, DRIVE_BACKWARD);
        }

        private void TurnLeft()
        {
            Network.SendUDP(ipAddress, port, TURN_LEFT);
        }

        private void TurnRight()
        {
            Network.SendUDP(ipAddress, port, TURN_RIGHT);
        }

        private void DriveForwardLeft()
        {
            Network.SendUDP(ipAddress, port, DRIVE_FORWARD_LEFT);
        }

        private void DriveForwardRight()
        {
            Network.SendUDP(ipAddress, port, DRIVE_FORWARD_RIGHT);
        }

        private void DriveBackwardLeft()
        {
            Network.SendUDP(ipAddress, port, DRIVE_BACKWARD_LEFT);
        }
        private void DriveBackwardRight()
        {
            Network.SendUDP(ipAddress, port, DRIVE_BACKWARD_RIGHT);
        }

        public void OnDestinationAddressPortChange(string ipAddress, string port)
        {
            this.ipAddress = ipAddress;
            this.port = port;
        }
    }
}
