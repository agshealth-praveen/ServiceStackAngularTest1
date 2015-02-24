using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vhsConnectMonitor.ServiceModel
{
    [Route("/ws_instance")]
    public class AllWsInstances:IReturn<List<WsInstance>>
    {
    }

    [Route("/ws_instance/{ClientId}")]
    public class GetWsInstance : IReturn<WsInstance>
    {
        public string ClientId { get; set; }
    }


    [Route("/ws_instance/{ClientId}/ping")]
    public class PingWsInstance : IReturn<string>
    {
        public string ClientId { get; set; }
    }
}
