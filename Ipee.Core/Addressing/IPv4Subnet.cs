using System;
using System.Collections.Generic;

namespace Ipee.Core.Addressing
{
    public class IPv4Subnet
    {
        private IPv4Address address;
        private IPv4SubnetMask mask;

        public IPv4Value NetAddress => address & mask;

        public IPv4Value BroadcastAddress => address | IPv4SubnetMask.Invert(mask);

        public IPv4Value HostAddress => IPv4Address.Increase(NetAddress, 1);

        /// <summary>
        /// Gibt alle Addressen in Form <see cref="IPv4Value"/>  aus, welche sich zwischen der errechneten HostAddress und der BroadcastAddress befinden.
        /// </summary>
        public IEnumerable<IPv4Value> AllPossibleAddresses
        {
            get
            {
                var current = HostAddress;

                while (current < BroadcastAddress)
                {
                    current = IPv4Value.Increase(current, 1);
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