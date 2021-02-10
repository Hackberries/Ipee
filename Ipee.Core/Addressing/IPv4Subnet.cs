using System;
using System.Collections.Generic;

namespace Ipee.Core.Addressing
{
    public class IPv4Subnet
    {
        private IPv4Address address;
        private IPv4SubnetMask mask;

        public IPv4Base NetAddress => address & mask;

        public IPv4Base BroadcastAddress => address | IPv4SubnetMask.Invert(mask);

        public IPv4Base HostAddress => IPv4Address.Increase(NetAddress, 1);

        public IEnumerable<IPv4Base> AllPossibleAddresses
        {
            get
            {
                var current = HostAddress;

                while (current < BroadcastAddress)
                {
                    current = IPv4Base.Increase(current, 1);
                    yield return current;
                }
            }
        }

        public IPv4Subnet(IPv4Address address, IPv4SubnetMask mask)
        {
            this.address = address;
            this.mask = mask;
        }
    }
}