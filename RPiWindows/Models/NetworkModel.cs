using RPiWindows.LANClients;

namespace RPiWindows.Models
{
    class NetworkModel
    {
        private static NetworkModel instance = null;
        private static readonly object padlock = new object();

        private NetworkModel()
        {
        }

        public static NetworkModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new NetworkModel();
                    }
                    return instance;
                }
            }
        }

        public string IpAddress { get; set; }
        public string Port { get; set; }
        public IClient NetworkClient { get; set; }
    }
}

