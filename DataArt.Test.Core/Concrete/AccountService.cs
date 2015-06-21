using System;
using System.Security.Cryptography;
using System.Text;
using DataArt.Test.Core.Abstract;
using DataArt.Test.Core.Domain;

namespace DataArt.Test.Core.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<User> _repository;

        public AccountService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public bool CheckCardExist(string cardNumber)
        {
            return _repository.Exists(u => u.CardNumber == cardNumber);
        }

        public bool CheckCardNotBlocked(string number)
        {
            var user = _repository.Get(u => u.CardNumber == number);
            if (user == null) { throw new ArgumentException("CardNumber");}
            return !user.Blocked;
        }

        public bool CheckPin(string cardNumber, string pin)
        {
            var pinHashed = ToMd5(pin);
            var res = _repository.Exists(u => u.CardNumber == cardNumber && u.Pin == pinHashed);
            if (!res)
            {
                UpadateAttemptOrBlockCard(cardNumber);
            }
            return res;
        }

        private void UpadateAttemptOrBlockCard(string cardNumber)
        {
            var user = _repository.Get(u => u.CardNumber == cardNumber);
            if (user.Atempts >= 4)
            {
                user.Blocked = true;
            }
            user.Atempts++;
            _repository.Update(user);
        }

        public User GetUser(string cardNumber)
        {
            return _repository.Get(u => u.CardNumber == cardNumber);
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