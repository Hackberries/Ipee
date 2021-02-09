using System.Globalization;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Ipee.Core.Addressing
{
    public class IPv4SubnetMask : IPv4Base
    {
        public IPv4SubnetMask(string address) : base(address)
        {
        }

        private IPv4SubnetMask(byte[] bytes) : base(bytes)
        {
        }

        public static IPv4SubnetMask ByBitCount(int bitCount)
        {
            if (bitCount < 1 || bitCount > 32)
                throw new Exception("Only specifications between 1 and 32 bits are valid.");

            BitArray bits = new BitArray(length: 32);
            for (int bitIndex = 32; bitIndex > 32 - bitCount; bitIndex--)
                bits.Set(bitIndex - 1, true);

            byte[] bytes = new byte[4];
            bits.CopyTo(bytes, 0);
            Array.Reverse(bytes);

            return new IPv4SubnetMask(bytes);
        }
    }
}