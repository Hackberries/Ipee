using Ipee.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace Ipee.Core.Addressing
{
    public class IPv4Addressnet
    {
        private IPv4Address address;
        private IPv4SubnetMask mask;

        private List<IPv4Value> givenAddresses = new();
        private List<IPv4Addressnet> subnets = new();

        public IEnumerable<IPv4Value> GivenAddresses => givenAddresses;

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

                    if (!givenAddresses.Contains(current))
                        yield return current;
                }
            }
        }

        public IPv4Addressnet(IPv4Address address, IPv4SubnetMask mask)
        {
            this.address = address;
            this.mask = mask;
        }

        /// <summary>
        /// Fügt eine neue IpAdresse hinzu.
        /// </summary>
        /// <exception cref="AddressAlreadyExistException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <param name="address">Die Adresse, welche hinzugefügt werden soll.</param>
        public void AddAddress(IPv4Address address)
        {
            if (address is null)
                throw new NullReferenceException();

            if (givenAddresses.Contains(address))
                throw new AddressAlreadyExistException();

            if (IsNotInRange(address))
                throw new AddressOutOfRangeException();

            givenAddresses.Add(address);
        }

        /// <summary>
        /// Fügt eine neue IpAdresse hinzu.
        /// </summary>
        /// <exception cref="AddressAlreadyExistException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <param name="address">Die Adresse, welche hinzugefügt werden soll.</param>
        public void RemoveAddress(IPv4Address address)
        {
            if (address is null)
                throw new NullReferenceException();

            givenAddresses.Remove(address);
        }

        public void AddSubnet(IPv4Addressnet subnet)
        {
            subnets.Add(subnet);
        }

        private bool IsInRange(IPv4Value address) => address > HostAddress || address < BroadcastAddress;

        private bool IsNotInRange(IPv4Value address) => address <= HostAddress || address >= BroadcastAddress;
    }
}