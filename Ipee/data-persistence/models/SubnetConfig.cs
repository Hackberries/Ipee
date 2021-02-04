using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.data_persistence.models
{
    public class SubnetConfig
    {
        public string Description { get; set; }
        public string SubnetIp { get; set; }
        public List<string> IpAddresses { get; set; }
    }
}
