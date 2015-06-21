using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using DataArt.Test.Core.Domain;

namespace DataArt.Test.Core.Concrete
{
    public static class MockData
    {
        public static List<User> GetUsers()
        {
            return new List<User>
            {
                new User
                {
                    UserName = "Valentyn Vynogradskyi",
                    CardNumber = "1111-1111-1111-1111".Replace("-",""),
                    Pin = ToMd5("1234"),
                    Blocked = false,
                    Balance = 10000
                },
                new User
                {
                    UserName = "Fool",
                    CardNumber = "1111-1111-1111-1112".Replace("-",""),
                    Pin = ToMd5("1232"),
                    Blocked = true,
                    Balance = 10000
                }
            };


        }
        public static string ToMd5(string input)
        {
            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}