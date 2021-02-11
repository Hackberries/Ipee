using System.Globalization;
using System;
using System.Collections.Generic;
using System.Collections;
using Ipee.Core.Exceptions;

namespace Ipee.Core.Addressing
{
    /// <summary>
    /// Stellt eine Subnetzmaske dar.
    /// </summary>
    public class IPv4SubnetMask : IPv4Base
    {
        public IPv4SubnetMask(string address) : base(address)
        {
        }

        private IPv4SubnetMask(byte[] bytes) : base(bytes)
        {
        }

        /// <summary>
        /// Erzeugt ein IPv4SubnetMask-Objekt anhand des Bit-Suffix.
        /// </summary>
        /// <param name="bitCount">Anzahl an Bits aufgrund welcher die IPv4SubnetMask erzeugt werden soll.</param>
        /// <exception cref="InvalidSubnetMaskBitCountException">Wird geworfen, wenn eine ungülige Anzahl an Bits angegeben wird.</exception>
        public static IPv4SubnetMask ByBitCount(int bitCount)
        {
            if (bitCount < 1 || bitCount > 32)
                throw new InvalidSubnetMaskBitCountException();

            BitArray bits = new BitArray(length: 32);
            for (int bitIndex = 32; bitIndex > 32 - bitCount; bitIndex--)
                bits.Set(bitIndex - 1, true);

            byte[] bytes = new byte[4];
            bits.CopyTo(bytes, 0);

            return new IPv4SubnetMask(bytes);
        }

        /// <summary>
        /// Gibt ein neues IPv4SubnetMask-Objekt zurück, dessen Bits das invertierte Ergebniss darstellen.
        /// </summary>
        /// <param name="mask">Auf dessen Basis werden die Bits invertiert.</param>
        /// <returns>Das invertierte IPv4SubnetMask-Objekt.</returns>
        /// <example>
        ///
        /// <code>
        /// var foo = IPv4SubnetMask.Invert(new IPv4SubnetMask("255.255.252.0"))
        /// </code>
        ///
        /// 255         . 255       . 252       . 0 <br />
        /// 1111 1111   1111 1111   1111 1100   0000 0000 <br />
        ///<br />
        /// Wird zu:<br />
        ///<br />
        /// 0           . 0         . 3       . 255<br />
        /// 0000 0000   0000 0000   0000 0011   1111 1111
        /// </example>
        public static IPv4SubnetMask Invert(IPv4SubnetMask mask)
            => new IPv4SubnetMask(BitConverter.GetBytes(~mask.ToInt()));
    }
}