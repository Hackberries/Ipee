using System.Globalization;
using System;
using System.Collections.Generic;

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
            var maxValue = (uint)(Math.Pow(2.0, (double)bitCount) - 1);
            var bytes = BitConverter.GetBytes(maxValue);
            return new IPv4SubnetMask(bytes);
        }
    }
}