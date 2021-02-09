using System;
using System.Collections.Generic;

namespace Ipee.Core.Addressing
{
    public class IPv4Base
    {
        private readonly byte[] bytes;

        /// <summary>
        /// Erstellt aus dem angegebenen String eine neue IPv4Base-Instanz.<br />
        /// Siehe <seealso cref="IPv4Base.Parse(string)"/> für genaueres.
        /// </summary>
        public IPv4Base(string address) => this.bytes = Parse(address);

        protected IPv4Base(byte[] bytes) => this.bytes = bytes;

        /// <summary>
        /// Ließt den angegebenen String als ein Byte-Array ein.<br />
        /// Zum Beispiel wird "192.168.10.10" zu "new byte() { 192, 168, 10, 10}".
        /// </summary>
        /// <param name="address">Der string, welcher als Byte-Array eingelesen werden soll. Zum Beispiel "192.168.10.10"</param>
        /// <example>
        /// <code>
        /// var bytes = IPv4Base.Parse("192.168.10.10");
        /// </code>
        /// </example>
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

        /// <summary>
        /// Vergleicht diese Instanz mit der angegebenen. Dafür werden jeweils die 4 <see cref="IPv4Base.bytes"/> miteinander verglichen.<br/>
        /// Gibt immer false zurück, wenn es sich bei dem angegebenen Objekt nicht um ein <see cref="IPv4Base"/> handelt.
        /// </summary>
        /// <param name="obj">Die Instanz, mit der verglichen werden soll.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is IPv4Base address)
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
        /// Führt eine UND-Operation zwischen den beiden angegebenen IPv4Base-Objekten durch.
        /// Dafür werden die IPv4Base-Objekte zuvor in ein Integer umgewandelt und die eigentliche
        /// UND-Operation wird dann mit den Integer durchgeführt. Das Ergebniss wird anschließend wieder
        /// in ein IPv4Base-Objekt zurückgewandelt.
        /// </summary>
        public static IPv4Base operator &(IPv4Base left, IPv4Base right)
            => new IPv4Base(BitConverter.GetBytes(left.ToInt() & right.ToInt()));

        /// <summary>
        /// Führt eine ODER-Operation zwischen den beiden angegebenen IPv4Base-Objekten durch.
        /// Dafür werden die IPv4Base-Objekte zuvor in ein Integer umgewandelt und die eigentliche
        /// ODER-Operation wird dann mit den Integer durchgeführt. Das Ergebniss wird anschließend wieder
        /// in ein IPv4Base-Objekt zurückgewandelt.
        /// </summary>
        public static IPv4Base operator |(IPv4Base left, IPv4Base right)
            => new IPv4Base(BitConverter.GetBytes(left.ToInt() | right.ToInt()));
    }
}
