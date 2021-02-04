using System.Collections.Generic;

namespace Ipee.data_persistence.models
{
    public class SubnetConfig
    {
        public string Description { get; set; }
        public string SubnetIp { get; set; }
        public List<string> IpAddresses { get; set; }
    }
}
