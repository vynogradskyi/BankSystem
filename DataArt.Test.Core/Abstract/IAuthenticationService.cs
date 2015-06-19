using DataArt.Test.Core.Domain;

namespace DataArt.Test.Core.Abstract
{
    public interface IAuthenticationService
    {
        bool CheckCardExist(string cardNumber);
        bool CheckCardNotBlocked(string number);
        bool CheckPin(string cardNUmber, string pin);
        User GetUser(string cardNUmber);
    }
}