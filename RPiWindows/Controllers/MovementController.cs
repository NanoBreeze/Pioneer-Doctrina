using RPiWindows.LANClients;
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
                    MovementModel.Instance.IsTurningLeft,
                    MovementModel.Instance.IsTurningRight,
                    MovementModel.Instance.IsDrivingForward,
                    MovementModel.Instance.IsDrivingBackward
                );
            }
        }

        private void HandleNavigation(bool isTurningLeft, bool isTurningRight, bool isDrivingForward, bool isDrivingBackward)
        {
            string ipAddress = NetworkModel.Instance.IpAddress;
            string port = NetworkModel.Instance.Port;
            IClient networkClient = NetworkModel.Instance.NetworkClient;

            if (isDrivingForward && isTurningLeft)
            {
                networkClient.Send(ipAddress, port, DRIVE_FORWARD_LEFT);
            }
            else if (isDrivingForward && isTurningRight)
            {
                networkClient.Send(ipAddress, port, DRIVE_FORWARD_RIGHT);
            }
            else if (isDrivingBackward && isTurningLeft)
            {
                networkClient.Send(ipAddress, port, DRIVE_BACKWARD_LEFT);
            }
            else if (isDrivingBackward && isTurningRight)
            {
                networkClient.Send(ipAddress, port, DRIVE_BACKWARD_RIGHT);
            }
            else if (isDrivingForward)
            {
                networkClient.Send(ipAddress, port, DRIVE_FORWARD);
            }
            else if (isDrivingBackward)
            {
                networkClient.Send(ipAddress, port, DRIVE_BACKWARD);
            }
            else if (isTurningLeft)
            {
                networkClient.Send(ipAddress, port, TURN_LEFT);
            }
            else if (isTurningRight)
            {
                networkClient.Send(ipAddress, port, TURN_RIGHT);
            }
        }
    }
}
