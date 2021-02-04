using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Store
{
    /// <summary>
    /// Wird geworfen, wenn eine Adresse bereits vorhanden ist.
    /// Siehe <see cref="IPAddressStore.AddAddress(System.Net.IPAddress)"/>.
    /// </summary>
    internal class AdressAlreadyExistException : Exception
    {
    }
}