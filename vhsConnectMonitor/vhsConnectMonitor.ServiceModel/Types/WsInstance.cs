using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vhsConnectMonitor.ServiceModel
{
    public class WsInstance
    {
        public string ClientId { get; set; }

        public WsStatus Status { get; set; }

    }

    public class WsStatus
    {
        public string Version { get; set; }
        
        public bool MqRunning { get; set; }

        public bool DbRunning { get; set; }

    }
}
