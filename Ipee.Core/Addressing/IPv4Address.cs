using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Core.Addressing
{
    public class IPv4Address
    {
        private readonly byte[] octets;

        public IPv4Address(string address)
        {
            this.octets = Parse(address);
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
                return this.octets[0] == address.octets[0]
                    && this.octets[1] == address.octets[1]
                    && this.octets[2] == address.octets[2]
                    && this.octets[3] == address.octets[3];
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => HashCode.Combine(octets);

        public override string ToString()
        {
            return $"{this.octets[0]}.{this.octets[1]}.{this.octets[2]}.{this.octets[3]}";
        }
    }
}