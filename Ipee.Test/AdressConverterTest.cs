using Ipee.Core.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ipee.Test
{
    public class AdressConverterTest
    {
        [Theory]
        [InlineData("192.168.0.1", "0:0:0:0:0:FFFF:C0A8:01")]
        [InlineData("192.168.0.255", "0:0:0:0:0:FFFF:C0A8:0FF")]
        [InlineData("253.21.161.14", "0:0:0:0:0:FFFF:FD15:A1E")]
        [InlineData("78.208.127.94", "0:0:0:0:0:FFFF:4ED0:7F5E")]
        [InlineData("217.76.148.213", "0:0:0:0:0:FFFF:D94C:94D5")]
        public void CanConvertToHex(string ip, string expectedHex)
        {
            Assert.Equal(expectedHex, AdressConverter.IPtoHex(ip));
        }
    }
}