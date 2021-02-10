using System;

namespace Ipee.Core.Exceptions
{
    /// <summary>
    /// Wird geworfen, wenn eine Adresse bereits vorhanden ist.
    /// Siehe <see cref="IPAddressStore.AddAddress(System.Net.IPAddress)"/>.
    /// </summary>
    internal class AddressAlreadyExistException : Exception
    {
    }
}