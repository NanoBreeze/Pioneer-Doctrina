namespace RPiWindows.Controllers
{
    class CameraController 
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
