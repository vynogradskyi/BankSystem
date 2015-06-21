using System;
using DataArt.Test.Core.Abstract;
using DataArt.Test.Core.Domain;

namespace DataArt.Test.Core.Concrete
{
    public class OperationsService : IOperationsService
    {
        private readonly IRepository<User> _profileRepository;

        public OperationsService(IRepository<User> profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public bool GetMoney(int userId, int amount)
        {
            var user = _profileRepository.Get(u => u.Id == userId);
            if (!(amount < user.Balance)) return false;
            user.Balance = user.Balance - amount;
            user.Operations.Add(new Operation
            {
                OperationType = OperationType.GetMoney,
                PerformTime = DateTime.Now,
                AdditionInformation = amount.ToString()
            });
            //Perform operation that withdraws money :))))
            _profileRepository.Update(user);
            return true;
        }

        public User Balance(int userId)
        {
            return _profileRepository.Get(u => u.Id == userId);
        }
    }
}