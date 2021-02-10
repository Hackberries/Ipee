using System;

namespace Ipee.Core.Addressing
{
    public class IPv4Subnet
    {
        private IPv4Address address;
        private IPv4SubnetMask mask;

        

        public IPv4Subnet(IPv4Address address, IPv4SubnetMask mask)
        {
            this.address = address;
            this.mask = mask;
        }


    }
}