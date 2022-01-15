using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Service
{
    public static class Extension
    {
        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static decimal CalculatePrice(decimal price, int userCount)
        {
            return price / userCount;
        }


        public static string EncodeBase64(this string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        public static string DecodeBase64(this string value)
        {
            var valueBytes = System.Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }
    }
}
