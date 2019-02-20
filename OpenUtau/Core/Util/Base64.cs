﻿using System.Collections.Generic;
using System.Text;

namespace OpenUtau.Core.Util
{
    internal static class Base64
    {
        public static string Base64EncodeInt12(int[] data)
        {
            List<string> l = new List<string>();
            foreach (int d in data)
            {
                l.Add(Base64EncodeInt12(d));
            }

            StringBuilder base64 = new StringBuilder();
            string last = string.Empty;
            int dups = 0;
            foreach (string b in l)
            {
                if (last == b)
                {
                    dups++;
                }
                else if (dups == 0)
                {
                    base64.Append(b);
                }
                else
                {
                    base64.Append('#');
                    base64.Append(dups + 1);
                    base64.Append('#');
                    dups = 0;
                    base64.Append(b);
                }
                last = b;
            }
            if (dups != 0)
            {
                base64.Append('#');
                base64.Append(dups + 1);
                base64.Append('#');
            }
            return base64.ToString();
        }

        private const string intToBase64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        private static string Base64EncodeInt12(int data)
        {
            if (data < 0)
            {
                data += 4096;
            }

            char[] base64 = new char[2];
            base64[0] = intToBase64[(data >> 6) & 0x003F];
            base64[1] = intToBase64[data & 0x003F];
            return new string(base64);
        }
    }
}