using Ipee.Core.Addressing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ipee.Test
{
    public class IPv4AddressnetTest
    {
        [Fact]
        public void CanCalculateNetAddress()
        {
            var address = new IPv4Address("192.168.10.5");
            var mask = new IPv4SubnetMask("255.255.252.0");

            var subnet = new IPv4Addressnet(address, mask);

            Assert.Equal("192.168.8.0", subnet.NetAddress.ToString());
        }

        [Fact]
        public void CanCalculateBroadcastAddress()
        {
            var address = new IPv4Address("192.168.10.5");
            var mask = new IPv4SubnetMask("255.255.252.0");

            var subnet = new IPv4Addressnet(address, mask);

            Assert.Equal("192.168.11.255", subnet.BroadcastAddress.ToString());
        }

        [Fact]
        public void CanCalculateHostAddress()
        {
            var address = new IPv4Address("192.168.10.5");
            var mask = new IPv4SubnetMask("255.255.252.0");

            var subnet = new IPv4Addressnet(address, mask);

            Assert.Equal("192.168.8.1", subnet.HostAddress.ToString());
        }

        [Fact]
        public void CanCalculateAllPossibleAddresses()
        {
            var address = new IPv4Address("192.168.10.5");
            var mask = new IPv4SubnetMask("255.255.252.0");

            var subnet = new IPv4Addressnet(address, mask);
            Assert.Equal(1022, subnet.AllPossibleAddresses.Count());

            subnet.AddAddress(new IPv4Address("192.168.10.7"));
            subnet.AddAddress(new IPv4Address("192.168.10.8"));

            Assert.Equal(1020, subnet.AllPossibleAddresses.Count());
        }

        [Fact]
        public void CanAddAddresses()
        {
            var address = new IPv4Address("192.168.10.5");
            var mask = new IPv4SubnetMask("255.255.252.0");
            var subnet = new IPv4Addressnet(address, mask);

            var address1 = new IPv4Address("192.168.10.7");
            var address2 = new IPv4Address("192.168.10.8");

            subnet.AddAddress(address1);
            subnet.AddAddress(address2);

            Assert.Contains(address1, subnet.GivenAddresses);
            Assert.Contains(address2, subnet.GivenAddresses);
        }

        [Fact]
        public void CanRemoveAddresses()
        {
            var address = new IPv4Address("192.168.10.5");
            var mask = new IPv4SubnetMask("255.255.252.0");
            var subnet = new IPv4Addressnet(address, mask);

            var address1 = new IPv4Address("192.168.10.7");
            subnet.AddAddress(address1);
            subnet.RemoveAddress(new IPv4Address("192.168.10.7"));

            Assert.DoesNotContain(address1, subnet.GivenAddresses);
        }

        [Fact]
        public void ThrowsExceptionWhenAddingWrongAddresses()
        {
            var address = new IPv4Address("192.168.10.5");
            var mask = new IPv4SubnetMask("255.255.252.0");
            var subnet = new IPv4Addressnet(address, mask);

            Assert.ThrowsAny<Exception>(() => subnet.AddAddress(new IPv4Address("192.168.8.1")));
            Assert.ThrowsAny<Exception>(() => subnet.AddAddress(new IPv4Address("192.0.0.0")));
            Assert.ThrowsAny<Exception>(() => subnet.AddAddress(null));

            var address1 = new IPv4Address("192.168.10.7");
            subnet.AddAddress(address1);
            Assert.ThrowsAny<Exception>(() => subnet.AddAddress(address1));
        }
    }
}