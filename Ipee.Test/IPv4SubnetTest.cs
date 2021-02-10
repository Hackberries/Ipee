using Ipee.Core.Addressing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ipee.Test
{
    public class IPv4SubnetTest
    {
        [Fact]
        public void CanCalculateNetAddress()
        {
            var address = new IPv4Address("192.168.10.5");
            var mask = new IPv4SubnetMask("255.255.252.0");

            var subnet = new IPv4Subnet(address, mask);

            Assert.Equal("192.168.8.0", subnet.NetAddress.ToString());
        }
    }
}