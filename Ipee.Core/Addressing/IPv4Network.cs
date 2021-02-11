using Ipee.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ipee.Core.Addressing
{
    public class IPv4Network
    {
        public IPv4Network MotherNetwork { get; set; }

        public IPv4Address SourceAddress { get; private set; }
        public IPv4SubnetMask SubnetMask { get; private set; }

        private List<IPv4Value> givenAddresses = new();
        private List<IPv4Network> subnets = new();

        public IEnumerable<IPv4Value> GivenAddresses
        {
            get
            {
                foreach (var address in givenAddresses)
                    yield return address;

                foreach (var subnet in subnets)
                    foreach (var address in subnet.GivenAddresses)
                        yield return address;
            }
        }

        public IPv4Value NetAddress => SourceAddress & SubnetMask;

        public IPv4Value BroadcastAddress => SourceAddress | IPv4SubnetMask.Invert(SubnetMask);

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

                    var isPossible = !givenAddresses.Contains(current)
                                    && !ExistInSubnet(current)
                                    && !IsPossibleInSubnet(current);

                    if (isPossible)
                        yield return current;
                }
            }
        }

        public IPv4Network(IPv4Address address, IPv4SubnetMask mask)
        {
            this.SourceAddress = address;
            this.SubnetMask = mask;
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

            if (givenAddresses.Contains(address) || ExistInSubnet(address))
                throw new AddressAlreadyExistException();

            if (IsNotInRange(address))
                throw new AddressOutOfRangeException();

            address.Network = this;
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

            address.Network = null;

            givenAddresses.Remove(address);
        }

        public void AddSubnet(IPv4Network subnet)
        {
            if (subnet is null)
                throw new ArgumentNullException(nameof(subnet));

            if (subnets.Contains(subnet))
                throw new NetworkAlreadyExistException();

            if (IsNotInRange(subnet.NetAddress))
                throw new NetworkOutOfRangeException();

            subnet.MotherNetwork = this;
            subnets.Add(subnet);
        }

        private bool IsInRange(IPv4Value address) => address > HostAddress || address < BroadcastAddress;

        private bool IsNotInRange(IPv4Value address) => address <= HostAddress || address >= BroadcastAddress;

        private bool ExistInSubnet(IPv4Value address)
        {
            foreach (var subnet in this.subnets)
                if (subnet.GivenAddresses.Contains(address) || address == subnet.HostAddress || address == subnet.BroadcastAddress)
                    return true;

            return false;
        }

        private bool IsPossibleInSubnet(IPv4Value address)
        {
            foreach (var subnet in this.subnets)
                if (subnet.AllPossibleAddresses.Contains(address) || address == subnet.HostAddress || address == subnet.BroadcastAddress)
                    return true;

            return false;
        }
    }
}