using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Core.Addressing
{
    public class IPv4Address
    {
        private readonly byte[] bytes;

        public IPv4Address(string address)
        {
            this.bytes = Parse(address);
        }

        public static byte[] Parse(string address)
        {
            var stringOctets = address.Split('.');

            if (stringOctets.Length != 4)
                throw new Exception();

            var output = new List<byte>();

            foreach (var octet in stringOctets)
                output.Add(byte.Parse(octet));

            return output.ToArray();
        }

        public override bool Equals(object obj)
        {
            if (obj is IPv4Address address)
            {
                return this.bytes[0] == address.bytes[0]
                    && this.bytes[1] == address.bytes[1]
                    && this.bytes[2] == address.bytes[2]
                    && this.bytes[3] == address.bytes[3];
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => HashCode.Combine(bytes);

        public override string ToString()
        {
            return $"{this.bytes[0]}.{this.bytes[1]}.{this.bytes[2]}.{this.bytes[3]}";
        }
    }
}