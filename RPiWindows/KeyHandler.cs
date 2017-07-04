using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using RPiWindows.Models;

/*
Problem: The movement controller is called only when OnKeyPressUp(..) and OnKeyPressDown(..) are called. These are only called 
when the Windows API detects key presses or releases. This problem was encountered in the previous version:
When the user presses and holds Up, OnKeyPressDown(..) is continuously called (with Up as a param).
Then, if the user presses Left, OnKeyPressDown(..) is called again but with Down as a parameter.
When the user releases Left, OnKeyPressUp(..) is called, and OnKeyPressDown(..) will not be called, even though we're still holding down the Up key! 
Thus, we still want to call the movement controller but since neither OnKeyPressUp(..) nor OnKeyPressDown(..) are called, we're unable to do so.
The solution is to continuously monitor if any movement flags are set to true and then call the movement controller if they are
*/

namespace RPiWindows.Controllers
{
    class KeyHandler
    {
        const VirtualKey CAPTURE_PICTURE = VirtualKey.Space;
        const VirtualKey TURN_LEFT = VirtualKey.Left;
        const VirtualKey TURN_RIGHT = VirtualKey.Right;
        const VirtualKey DRIVE_FORWARD = VirtualKey.Up;
        const VirtualKey DRIVE_BACKWARD = VirtualKey.Down;

        private MainPage mainPage;
        public KeyHandler(MainPage mainPage)
        {
            this.mainPage = mainPage;
        }

        public void OnKeyPressDown(VirtualKey key)
        {
            switch (key)
            {
                case CAPTURE_PICTURE:
                    throw new NotImplementedException();
                    return;
                case TURN_LEFT:
                    MovementModel.Instance.IsTurningLeft = true;
                    MovementModel.Instance.IsTurningRight= false;
                    mainPage.ShowLeftGraphic();
                    mainPage.ShowStopRightGraphic();
                    break;
                case TURN_RIGHT:
                    MovementModel.Instance.IsTurningRight= true;
                    MovementModel.Instance.IsTurningLeft = false;
                    mainPage.ShowRightGraphic();
                    mainPage.ShowStopLeftGraphic();
                    break;
                case DRIVE_FORWARD:
                    MovementModel.Instance.IsDrivingForward = true;
                    MovementModel.Instance.IsDrivingBackward = false;
                    mainPage.ShowForwardGraphic();
                    mainPage.ShowStopBackwardGraphic();
                    break;
                case DRIVE_BACKWARD:
                    MovementModel.Instance.IsDrivingBackward = true;
                    MovementModel.Instance.IsDrivingForward = false;
                    mainPage.ShowBackwardGraphic();
                    mainPage.ShowStopForwardGraphic();
                    break;
            }
        }

        public void OnKeyPressUp(VirtualKey key)
        {
            switch (key)
            {
                case TURN_LEFT:
                    MovementModel.Instance.IsTurningLeft = false;
                    mainPage.ShowStopLeftGraphic();
                    break;
                case TURN_RIGHT:
                    MovementModel.Instance.IsTurningRight = false;
                    mainPage.ShowStopRightGraphic();
                    break;
                case DRIVE_FORWARD:
                    MovementModel.Instance.IsDrivingForward = false;
                    mainPage.ShowStopForwardGraphic();
                    break;
                case DRIVE_BACKWARD:
                    MovementModel.Instance.IsDrivingBackward = false;
                    mainPage.ShowStopBackwardGraphic();
                    break;
            }
        }
    }
}

