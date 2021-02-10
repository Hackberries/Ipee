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

        [Theory]
        [InlineData("abc.lorem.ipsum.WerDasLießtIstDoof")]
        [InlineData("300.999.0.1")]
        [InlineData("30099901")]
        [InlineData("30099......901")]
        [InlineData("192.168.01.")]
        public void ThrowsParseException(string input)
        {
            Assert.ThrowsAny<Exception>(() => new IPv4Address(input));
        }

        [Fact]
        public void AND_Operation()
        {
            var ip = new IPv4Address("253.21.161.14");
            var mask = new IPv4Address("255.255.255.0");

            var subnet = ip & mask;

            Assert.Equal("253.21.161.0", subnet.ToString());
        }

        [Fact]
        public void OR_Operation()
        {
            var ip = new IPv4Address("253.21.161.14");
            var mask = new IPv4Address("255.255.255.0");

            var subnet = ip | mask;

            Assert.Equal("255.255.255.14", subnet.ToString());
        }

        [Theory]
        [InlineData("192.168.0.0", 1, "192.168.0.1")]
        [InlineData("192.168.0.0", 2, "192.168.0.2")]
        [InlineData("192.168.8.0", 1, "192.168.8.1")]
        public void IncreaseCorrectly(string input, int increaseBy, string excpected)
        {
            var address = new IPv4Address(input);

            Assert.Equal(excpected, IPv4Address.Increase(address, increaseBy).ToString());
        }
    }
}