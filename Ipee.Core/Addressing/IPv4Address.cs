using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Core.Addressing
{
    /// <summary>
    /// Stellt ein IPv4 Addresse dar.
    /// </summary>
    public class IPv4Address : IPv4Value
    {
        public IPv4Address(string address) : base(address)
        {
        }

        protected IPv4Address(byte[] bytes) : base(bytes)
        {
        }
    }
}