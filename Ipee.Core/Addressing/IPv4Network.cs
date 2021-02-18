using Ipee.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Ipee.Core.Addressing
{
    /// <summary>
    /// Dieses Objekt soll ein komplettes Netzwerk von zugeordneten IP-Adressen, dessen Netz-, Broadcast- und Host-Adresse, sowie allen Subnetzen darstellen.
    /// </summary>
    public class IPv4Network
    {
        public string Description { get; set; }

        public int SubnetsCount => subnets.Count();

        public int IpAddressesCount => GivenAddresses.Count();

        /// <summary>
        /// Referenz auf das übergeordnete IPv4Network-Objekt. Siehe auch: <seealso cref="IPv4Network.Subnets"/>.
        /// </summary>
        [JsonIgnore]
        public IPv4Network MotherNetwork { get; set; }

        /// <summary>
        /// Die Adresse auf dessen Basis das Netzwerk erstellt wurde.
        /// </summary>
        public IPv4Address SourceAddress { get; init; }

        /// <summary>
        /// Die Subnetzmaske auf dessen Basis das Netzwerk erstellt wurde.
        /// </summary>
        public IPv4SubnetMask SubnetMask { get; init; }

        private List<IPv4Value> givenAddresses = new();
        private List<IPv4Network> subnets = new();

        /// <summary>
        /// Eine Iteration an allen vergebenen Adressen in diesem Netzwerk. <br/>
        /// Siehe auch: <seealso cref="IPv4Network.AllGivenAddresses"/>.
        /// </summary>
        public IEnumerable<IPv4Value> GivenAddresses
        {
            get => givenAddresses;

            init => givenAddresses = value.ToList();
        }

        /// <summary>
        /// Eine Iteration an allen vergebenen Adressen in diesem Netzwerk sowie allen vergebenen Adressen in den Subnetzen. <br/>
        /// Dies wird f�r die komplette Verschachtelungstiefe dieses Netzwerks, ab diesem Netzwerk durchgef�hrt.<br/>
        /// Siehe auch: <seealso cref="IPv4Network.GivenAddresses"/>.
        /// </summary>
        public IEnumerable<IPv4Value> AllGivenAddresses
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

        /// <summary>
        /// Eine Iteration an allen untergeordneten IPv4Network-Objekten dieses Netzwerkes.
        /// </summary>
        public IEnumerable<IPv4Network> Subnets { get => subnets; init => subnets = value.ToList(); }

        /// <summary>
        /// Netz-Adresse dieses Netzwerkes. Wird automatisch ermittelt.
        /// </summary>
        public IPv4Value NetAddress => SourceAddress & SubnetMask;

        /// <summary>
        /// Broadcast-Adresse dieses Netzwerkes. Wird automatisch ermittelt.
        /// </summary>
        public IPv4Value BroadcastAddress => SourceAddress | IPv4SubnetMask.Invert(SubnetMask);

        /// <summary>
        /// Host-Adresse dieses Netzwerkes. Wird automatisch ermittelt.
        /// </summary>
        public IPv4Value HostAddress => IPv4Address.Increase(NetAddress, 1);

        /// <summary>
        /// Gibt alle Addressen in Form <see cref="IPv4Value"/>  aus, welche sich zwischen der errechneten HostAddress und der BroadcastAddress befinden.
        /// </summary>
        [JsonIgnore]
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

        public IPv4Network(IPv4Address SourceAddress, IPv4SubnetMask SubnetMask)
        {
            this.SourceAddress = SourceAddress;
            this.SubnetMask = SubnetMask;
        }

        /// <summary>
        /// F�gt eine neue IpAdresse hinzu.
        /// </summary>
        /// <exception cref="AddressAlreadyExistException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <param name="address">Die Adresse, welche hinzugef�gt werden soll.</param>
        public void AddAddress(IPv4Value address)
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
        /// F�gt eine neue IpAdresse hinzu.
        /// </summary>
        /// <exception cref="AddressAlreadyExistException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <param name="address">Die Adresse, welche hinzugef�gt werden soll.</param>
        /// <example>
        /// <code>
        /// var address = AppStore.Instance.MainNetwork.AllGivenAddresses.Where(add => add.Address == "192.168.1.1").First();
        /// address.Network.RemoveAddress(address);
        /// </code>
        /// </example>
        public void RemoveAddress(IPv4Value address)
        {
            if (address is null)
                throw new NullReferenceException();

            address.Network = null;

            givenAddresses.Remove(address);
        }

        /// <summary>
        /// Wei�t dem Netzwerk ein untergeordnetes IPv4Network zu.
        /// </summary>
        /// <param name="subnet">Das IPv4Network welches untergeordnet werden soll.</param>
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
                if (subnet.AllGivenAddresses.Contains(address) || address == subnet.HostAddress || address == subnet.BroadcastAddress)
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

        public override string ToString()
        {
            return $"{this.Description} | Netz-Adresse: {this.NetAddress}";
        }
    }
}