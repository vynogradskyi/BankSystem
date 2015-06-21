using DataArt.Test.Core.Domain;

namespace DataArt.Test.Core.Abstract
{
    public interface IAccountService
    {
        bool CheckCardExist(string cardNumber);
        bool CheckCardNotBlocked(string number);
        bool CheckPin(string cardNumber, string pin);
        User GetUser(string cardNumber);
    }
}