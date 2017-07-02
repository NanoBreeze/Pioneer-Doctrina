using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPiWindows.Models
{
    class NetworkInfo
    {
        private static NetworkInfo instance = null;
        private static readonly object padlock = new object();

        private NetworkInfo()
        {
        }

        public static NetworkInfo Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new NetworkInfo();
                    }
                    return instance;
                }
            }
        }

        public string IpAddress { get; set; }
        public string Port { get; set; }
    }
}

