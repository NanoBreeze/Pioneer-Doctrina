using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace RPiWindows.Controllers
{
    class KeyHandler
    {
        private CameraController cameraController;
        private MovementController movementController;

        const VirtualKey CAPTURE_PICTURE = VirtualKey.Space;
        const VirtualKey TURN_LEFT = VirtualKey.Left;
        const VirtualKey TURN_RIGHT = VirtualKey.Right;
        const VirtualKey DRIVE_FORWARD = VirtualKey.Up;
        const VirtualKey DRIVE_BACKWARD = VirtualKey.Down;

        // I don't want to use qezx for combo controls
        private bool isTurningLeft;
        private bool isTurningRight;
        private bool isDrivingForward;
        private bool isDrivingBackward;


        public KeyHandler(MovementController movementController, CameraController cameraController)
        {
            this.movementController = movementController;
            this.cameraController = cameraController;

            isTurningLeft = false;
            isTurningRight = false;
            isDrivingForward = false;
            isDrivingBackward = false;
        }

        public void OnKeyPressDown(VirtualKey key)
        {
            switch (key)
            {
                case CAPTURE_PICTURE:
                    throw new NotImplementedException();
                    return;
                case TURN_LEFT:
                    isTurningLeft = true;
                    isTurningRight = false; // Set right to false since it won't make sense for car to turn left and right
                    movementController.HandleNavigation(isTurningLeft, isTurningRight, isDrivingForward, isDrivingBackward);
                    break;
                case TURN_RIGHT:
                    isTurningRight = true;
                    isTurningLeft = false;
                    movementController.HandleNavigation(isTurningLeft, isTurningRight, isDrivingForward, isDrivingBackward);
                    break;
                case DRIVE_FORWARD:
                    isDrivingForward = true;
                    isDrivingBackward = false;
                    movementController.HandleNavigation(isTurningLeft, isTurningRight, isDrivingForward, isDrivingBackward);
                    break;
                case DRIVE_BACKWARD:
                    isDrivingBackward = true;
                    isDrivingForward = false;
                    movementController.HandleNavigation(isTurningLeft, isTurningRight, isDrivingForward, isDrivingBackward);
                    break;
            }
        }

        public void OnKeyPressUp(VirtualKey key)
        {
            switch (key)
            {
                case TURN_LEFT:
                    isTurningLeft = false;
                    if (isDrivingForward)
                    {
                        while (true)
                        {
                            Debug.Write("TRUE");
                        }
                    }
                    movementController.HandleNavigation(isTurningLeft, isTurningRight, isDrivingForward, isDrivingBackward);
                    break;
                case TURN_RIGHT:
                    isTurningRight = false;
                    movementController.HandleNavigation(isTurningLeft, isTurningRight, isDrivingForward, isDrivingBackward);
                    break;
                case DRIVE_FORWARD:
                    isDrivingForward = false;
                    movementController.HandleNavigation(isTurningLeft, isTurningRight, isDrivingForward, isDrivingBackward);
                    break;
                case DRIVE_BACKWARD:
                    isDrivingBackward = false;
                    movementController.HandleNavigation(isTurningLeft, isTurningRight, isDrivingForward, isDrivingBackward);
                    break;
            }
        }
    }
}

