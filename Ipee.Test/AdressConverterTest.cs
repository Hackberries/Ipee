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
        [Fact]
        public void CanConvertToHex()
        {
            Assert.Equal("0:0:0:0:0:FFFF:C0A8:01", AdressConverter.IPtoHex("192.168.0.1"));
        }
    }
}