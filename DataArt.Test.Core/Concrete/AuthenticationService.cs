using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using DataArt.Test.Core.Abstract;
using DataArt.Test.Core.Domain;

namespace DataArt.Test.Core.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IProfileRepository _profileRepository;

        public AuthenticationService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
            var users = new List<User>
            {
                new User
                {
                    UserName = "Valentyn Vynogradskyi",
                    CardNumber = "1111-1111-1111-1111".Replace("-",""),
                    Pin = ToMd5("1234"),
                    Blocked = false
                },
                new User
                {
                    UserName = "Fool",
                    CardNumber = "1111-1111-1111-1112".Replace("-",""),
                    Pin = ToMd5("1232"),
                    Blocked = true
                }
            };
            _profileRepository.PopulateForTesting(users);
        }

        public bool CheckCardExist(string cardNumber)
        {
            return _profileRepository.Exists<User>(u => u.CardNumber == cardNumber);
        }

        public bool CheckCardNotBlocked(string number)
        {
            var user = _profileRepository.Get<User>(u => u.CardNumber == number);
            if (user == null) { throw new ArgumentException("CardNumber");}
            return !user.Blocked;
        }

        public bool CheckPin(string cardNUmber, string pin)
        {
            var pinHashed = ToMd5(pin);
            return _profileRepository.Exists<User>(u => u.CardNumber == cardNUmber && u.Pin == pinHashed);
        }

        public User GetUser(string cardNumber)
        {
            return _profileRepository.Get<User>(u => u.CardNumber == cardNumber);
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