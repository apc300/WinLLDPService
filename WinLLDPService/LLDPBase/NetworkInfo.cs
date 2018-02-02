﻿namespace WinLLDPService
{
    using System.Net;

    class NetworkInfo
    {
        /// <summary>
        /// Convert network mask to CIDR notation
        /// </summary>
        /// <param name="ip">Network mask</param>
        /// <returns></returns>
        public static int GetCIDRFromIPMaskAddress(IPAddress ip)
        {

            byte[] bytes = ip.GetAddressBytes();

            int cidrnet = 0;
            bool zeroed = false;

            foreach (var t in bytes)
            {
                for (int v = t; (v & 0xFF) != 0; v = v << 1)
                {
                    if (zeroed)
                    {
                        // invalid netmask
                        return ~cidrnet;
                    }

                    if ((v & 0x80) == 0)
                    {
                        zeroed = true;
                    }
                    else
                    {
                        cidrnet++;
                    }
                }
            }

            return cidrnet;
        }

        /// <summary>
        /// Change for example adapter link speed into more human readable format
        /// </summary>
        /// <param name="size"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static string ReadableSize(double size, int unit = 0)
        {
            string[] units = { "b", "Kb", "Mb", "Gb", "Tb", "Pb", "Eb", "Zb", "Yb" };

            while (size >= 1000)
            {
                size /= 1000;
                ++unit;
            }

            return string.Format("{0:G4}{1}", size, units[unit]);
        }


    }
}