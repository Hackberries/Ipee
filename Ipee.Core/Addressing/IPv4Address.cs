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

        public IPv4Address(string address) => this.bytes = Parse(address);

        private IPv4Address(byte[] bytes) => this.bytes = bytes;

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

        private int ToInt() => BitConverter.ToInt32(bytes);

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

        public override string ToString() => $"{this.bytes[0]}.{this.bytes[1]}.{this.bytes[2]}.{this.bytes[3]}";

        /// <summary>
        /// Führt eine UND-Operation zwischen den beiden angegebenen IPv4Address-Objekten durch.
        /// Dafür werden die IPv4Address-Objekte zuvor in ein Integer umgewandelt und die eigentliche
        /// UND-Operation wird dann mit den Integer durchgeführt. Das Ergebniss wird anschließend wieder
        /// in ein IPv4Address-Objekt zurückgewandelt.
        /// </summary>
        public static IPv4Address operator &(IPv4Address left, IPv4Address right)
            => new IPv4Address(BitConverter.GetBytes(left.ToInt() & right.ToInt()));

        /// <summary>
        /// Führt eine ODER-Operation zwischen den beiden angegebenen IPv4Address-Objekten durch.
        /// Dafür werden die IPv4Address-Objekte zuvor in ein Integer umgewandelt und die eigentliche
        /// ODER-Operation wird dann mit den Integer durchgeführt. Das Ergebniss wird anschließend wieder
        /// in ein IPv4Address-Objekt zurückgewandelt.
        /// </summary>
        public static IPv4Address operator |(IPv4Address left, IPv4Address right)
            => new IPv4Address(BitConverter.GetBytes(left.ToInt() | right.ToInt()));
    }
}