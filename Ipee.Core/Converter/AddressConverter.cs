using Ipee;
using Ipee.Core.Addressing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Core.Converter
{
    public class AddressConverter
    {
        /// <summary>
        /// Mappt eine IPv4 Adresse auf eine IPv6 Adresse.
        /// DIe IP wird gesplittet, dann in Binär umgewandelt und dann in Hex.
        /// Anschließend wird die fertige IPv6 Adresse wieder zusammengesetzt.
        /// </summary>
        /// <param name="IP">
        /// IPv4 Adresse im String Format.
        /// </param>
        /// <returns>
        /// Gibt die gemappte IPv6 Adresse im lowercase zurück.
        /// </returns>
        public static string IPv4toIPv6(string IP)
        {
            String IPv4 = IP;
            int[] SplitOctetsInt = Array.ConvertAll(IPv4.Split("."), s => int.Parse(s));
            string IPv6 = "0:0:0:0:0:FFFF:";
            List<string> IPBinary = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                string binary = Convert.ToString(SplitOctetsInt[i], 2);
                IPBinary.Add(binary);
                IPv6 = IPv6 + Convert.ToInt32(IPBinary[i], 2).ToString("X2");
                if (i == 1)
                {
                    IPv6 = IPv6 + ":";
                }
            }
            return IPv6.ToLower();
        }

        public static string IPv4toIPv6(IPv4Address address)
        {
            return IPv4toIPv6(address.ToString());
        }

        /// <summary>
        /// Wandelt die IPv4 Adresse in Binär um und gibt sie zurück.
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static string IPtoBinary(string IP)
        {
            string BinaryIP = "";
            int[] SplitOctetsInt = Array.ConvertAll(IP.Split("."), s => int.Parse(s));
            for (int i = 0; i < 4; i++)
            {
                string binary = Convert.ToString(SplitOctetsInt[i], 2).PadLeft(8, '0');
                BinaryIP += binary + " ";
            }
            return BinaryIP;
        }
    }
}