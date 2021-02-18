using Ipee.Core.Addressing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ipee.Test
{
    public class IPv4SubnetMaskTest
    {
        [Theory]
        [InlineData(32, "255.255.255.255")]
        [InlineData(24, "255.255.255.0")]
        [InlineData(16, "255.255.0.0")]
        [InlineData(9, "255.128.0.0")]
        public void CreatesByBitCountCorrectly(int bitCount, string expected)
        {
            var mask = IPv4SubnetMask.ByBitCount(bitCount);
            Assert.Equal(expected, mask.ToString());
        }

        [Theory]
        [InlineData(30, "255.255.255.224")]
        [InlineData(8190, "255.255.224.0")]
        [InlineData(500000, "255.248.0.0")]
        public void CreatesByAddressCountCorrectly(int addressCount, string expected)
        {
            var mask = IPv4SubnetMask.ByAddressCount(addressCount);
            Assert.Equal(expected, mask.ToString());
        }

        [Theory]
        [InlineData(33)]
        [InlineData(0)]
        [InlineData(-1)]
        public void ThrowsByBitCountException(int bitCount)
        {
            Assert.ThrowsAny<Exception>(() => IPv4SubnetMask.ByBitCount(bitCount));
        }

        [Theory]
        [InlineData("255.255.255.255", "0.0.0.0")]
        [InlineData("255.255.255.0", "0.0.0.255")]
        [InlineData("255.255.0.0", "0.0.255.255")]
        [InlineData("255.128.0.0", "0.127.255.255")]
        public void CanInvert(string subnet, string expected)
        {
            Assert.Equal(expected, IPv4SubnetMask.Invert(new IPv4SubnetMask(subnet)).ToString());
        }
    }
}