using Ipee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Core.Converter
{
    public class AdressConverter
    {
        public static string IPtoHex(string IP)
        {
            String IPv4 = IP;
            int[] SplitOctetsInt = Array.ConvertAll(IPv4.Split("."), s => int.Parse(s));
            string IPv6 = "0:0:0:0:0:FFFF:";
            List<string> IPBinary = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                string binary = Convert.ToString(SplitOctetsInt[i], 2);
                IPBinary.Add(binary);
            }
            for (int i = 0; i < IPBinary.Count; i++)
            {
                IPv6 = IPv6 + Convert.ToInt32(IPBinary[i], 2).ToString("X");
                if (i == 1)
                {
                    IPv6 = IPv6 + ":";
                }
            }
            return IPv6;
        }
    }
}