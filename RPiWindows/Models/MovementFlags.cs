using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RPiWindows.Models
{
    /// <summary>
    /// Thread-safe singleton
    /// </summary>
    class MovementFlags
    {
        private static MovementFlags instance = null;
        private static readonly object padlock = new object();

        private MovementFlags()
        {
            IsDrivingForward = false;
            IsDrivingBackward = false;
            IsTurningLeft = false;
            IsTurningRight = false;
        }

        public static MovementFlags Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MovementFlags();
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
