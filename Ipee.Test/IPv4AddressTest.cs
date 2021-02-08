using Ipee.Core.Addressing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ipee.Test
{
    public class IPv4AddressTest
    {
        [Theory]
        [InlineData("192.168.0.1")]
        [InlineData("192.168.0.255")]
        [InlineData("253.21.161.14")]
        [InlineData("78.208.127.94")]
        [InlineData("217.76.148.213")]
        public void IsConstructedCorrectly(string address)
        {
            var ip = new IPv4Address(address);
            Assert.Equal(address, ip.ToString());
        }

        [Fact]
        public void IsComparable()
        {
            var ip = new IPv4Address("253.21.161.14");
            var ip2 = new IPv4Address("253.21.161.14");
            var ip3 = new IPv4Address("192.168.0.1");
            Assert.Equal(ip, ip2);
            Assert.NotEqual(ip, ip3);
        }

        [Theory]
        [InlineData("253.21.161.14", new byte[] { 253, 21, 161, 14 })]
        [InlineData("192.168.0.1", new byte[] { 192, 168, 0, 1 })]
        [InlineData("78.208.127.94", new byte[] { 78, 208, 127, 94 })]
        public void CanParseFromString(string input, byte[] excpectedBytes)
        {
            var octets = IPv4Address.Parse(input);

            for (int index = 0; index < octets.Length; index++)
                Assert.Equal(excpectedBytes[index], octets[index]);
        }

        [Fact]
        public void ThrowsParseException()
        {
            Assert.ThrowsAny<Exception>(() => new IPv4Address("abc.lorem.ipsum.WerDasLießtIstDoof"));
        }
    }
}