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
        [InlineData(33)]
        [InlineData(0)]
        [InlineData(-1)]
        public void ThrowsByBitCountException(int bitCount)
        {
            Assert.ThrowsAny<Exception>(() => IPv4SubnetMask.ByBitCount(bitCount));
        }
    }
}