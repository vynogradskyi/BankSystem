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
        }

        public bool CheckCardExist(string cardNumber)
        {
            throw new System.NotImplementedException();
        }

        public bool CheckCardNotBlocked(string number)
        {
            throw new System.NotImplementedException();
        }

        public bool CheckPin(string cardNUmber, string pin)
        {
            throw new System.NotImplementedException();
        }

        public User GetUser(string cardNUmber)
        {
            throw new System.NotImplementedException();
        }
    }
}