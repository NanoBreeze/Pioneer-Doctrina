namespace RPiWindows.Models
{
    /// <summary>
    /// Thread-safe singleton
    /// </summary>
    class MovementModel
    {
        private static MovementModel instance = null;
        private static readonly object padlock = new object();

        private MovementModel()
        {
            IsDrivingForward = false;
            IsDrivingBackward = false;
            IsTurningLeft = false;
            IsTurningRight = false;
        }

        public static MovementModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MovementModel();
                    }
                    return instance;
                }
            }
        }

        public bool IsDrivingForward { get; set; }
        public bool IsDrivingBackward { get; set; }
        public bool IsTurningLeft { get; set; }
        public bool IsTurningRight{ get; set; }
    }
}
