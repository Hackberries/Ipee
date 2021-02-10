using Ipee.Core.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ipee.Test
{
    public class AddressConverterTest
    {
        [Theory]
        [InlineData("192.168.0.1", "0:0:0:0:0:ffff:c0a8:0001")]
        [InlineData("192.168.0.255", "0:0:0:0:0:ffff:c0a8:00ff")]
        [InlineData("253.21.161.14", "0:0:0:0:0:ffff:fd15:a10e")]
        [InlineData("78.208.127.94", "0:0:0:0:0:ffff:4ed0:7f5e")]
        [InlineData("217.76.148.213", "0:0:0:0:0:ffff:d94c:94d5")]
        public void CanConvertToHex(string ip, string expectedHex)
        {
            Assert.Equal(expectedHex, AddressConverter.IPtoHex(ip));
        }
    }
}