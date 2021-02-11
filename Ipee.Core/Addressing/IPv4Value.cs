using Ipee.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Ipee.Core.Addressing
{
    public class IPv4Value
    {
        private readonly byte[] _bytes = new byte[4];

        /// <summary>
        /// Erstellt aus dem angegebenen String eine neue IPv4Value-Instanz.<br />
        /// Siehe <seealso cref="IPv4Value.Parse(string)"/> für genaueres.
        /// </summary>
        public IPv4Value(string address)
        {
            var bytes = Parse(address).Reverse().ToArray();
            Array.Copy(bytes, _bytes, bytes.Length);
        }

        protected IPv4Value(byte[] bytes)
        {
            Array.Copy(bytes, _bytes, bytes.Length);
        }

        /// <summary>
        /// Ließt den angegebenen String als ein Byte-Array ein.<br />
        /// Zum Beispiel wird "192.168.10.10" zu "new byte() { 192, 168, 10, 10}".
        /// </summary>
        /// <param name="address">Der string, welcher als Byte-Array eingelesen werden soll. Zum Beispiel "192.168.10.10"</param>
        /// <example>
        /// <code>
        /// var bytes = IPv4Value.Parse("192.168.10.10");
        /// </code>
        /// </example>
        public static byte[] Parse(string address)
        {
            var stringOctets = address.Split('.');

            if (stringOctets.Length != 4)
                throw new IPv4ValueParsingException();

            var output = new List<byte>();

            foreach (var octet in stringOctets)
                output.Add(byte.Parse(octet));

            return output.ToArray();
        }

        protected int ToInt()
        {
            return BitConverter.ToInt32(_bytes);
        }

        /// <summary>
        /// Vergleicht diese Instanz mit der angegebenen. Dafür werden jeweils die 4 <see cref="IPv4Value._bytes"/> miteinander verglichen.<br/>
        /// Gibt immer false zurück, wenn es sich bei dem angegebenen Objekt nicht um ein <see cref="IPv4Value"/> handelt.
        /// </summary>
        /// <param name="obj">Die Instanz, mit der verglichen werden soll.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is IPv4Value address)
            {
                return this._bytes[0] == address._bytes[0]
                    && this._bytes[1] == address._bytes[1]
                    && this._bytes[2] == address._bytes[2]
                    && this._bytes[3] == address._bytes[3];
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Erzeugt ein neues <see cref="IPv4Value"/> Objekt auf Basis des angegebenen und erhöht es um einen bestimmten Wert.
        /// </summary>
        /// <param name="address">Die Basis, auf der erhöht werden soll.</param>
        /// <param name="increaseBy">Der Wert, um welche der Wert erhöht werden soll</param>
        /// <example>
        /// <code>
        /// var foo = IPv4Value.Increase(new IPv4Address("253.21.161.14"), 2); // = "253.21.161.16"
        /// </code>
        /// </example>
        public static IPv4Value Increase(IPv4Value address, int increaseBy)
        {
            var value = address.ToInt() + increaseBy;
            var bytes = BitConverter.GetBytes(value);

            return new IPv4Value(bytes);
        }

        public override int GetHashCode() => HashCode.Combine(_bytes);

        public override string ToString() => $"{this._bytes[3]}.{this._bytes[2]}.{this._bytes[1]}.{this._bytes[0]}";

        /// <summary>
        /// Führt eine UND-Operation zwischen den beiden angegebenen IPv4Value-Objekten durch.
        /// Dafür werden die IPv4Value-Objekte zuvor in ein Integer umgewandelt und die eigentliche
        /// UND-Operation wird dann mit den Integer durchgeführt. Das Ergebniss wird anschließend wieder
        /// in ein IPv4Value-Objekt zurückgewandelt.
        /// </summary>
        public static IPv4Value operator &(IPv4Value left, IPv4Value right)
            => new IPv4Value(BitConverter.GetBytes(left.ToInt() & right.ToInt()));

        /// <summary>
        /// Führt eine ODER-Operation zwischen den beiden angegebenen IPv4Value-Objekten durch.
        /// Dafür werden die IPv4Value-Objekte zuvor in ein Integer umgewandelt und die eigentliche
        /// ODER-Operation wird dann mit den Integer durchgeführt. Das Ergebniss wird anschließend wieder
        /// in ein IPv4Value-Objekt zurückgewandelt.
        /// </summary>
        public static IPv4Value operator |(IPv4Value left, IPv4Value right)
            => new IPv4Value(BitConverter.GetBytes(left.ToInt() | right.ToInt()));

        /// <summary>
        /// Prüft, ob das linke <see cref="IPv4Value"/> Objekt einen niedrigeren Wert bestitzt.
        /// </summary>
        /// <returns>Gibt true zurück, wenn Links kleiner ist.</returns>
        /// <example>
        /// <code>
        /// var foo = new IPv4Address("253.21.161.14") &lt; new IPv4Address("253.188.0.1"); //true
        /// var bar = new IPv4Address("253.21.161.14") &lt; new IPv4Address("253.0.161.14"); //false
        /// </code>
        /// </example>
        public static bool operator <(IPv4Value left, IPv4Value right)
            => left.ToInt() < right.ToInt();

        /// <summary>
        /// Prüft, ob das linke <see cref="IPv4Value"/> Objekt einen höheren Wert bestitzt.
        /// </summary>
        /// <returns>Gibt true zurück, wenn Links größer ist.</returns>
        /// <example>
        /// <code>
        /// var foo = new IPv4Address("253.21.161.14") &gt; new IPv4Address("253.188.0.1"); //true
        /// var bar = new IPv4Address("253.21.161.14") &gt; new IPv4Address("253.0.161.14"); //false
        /// </code>
        /// </example>
        public static bool operator >(IPv4Value left, IPv4Value right)
            => left.ToInt() > right.ToInt();
    }
}