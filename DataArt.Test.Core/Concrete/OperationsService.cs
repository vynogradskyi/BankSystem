using System;
using System.Collections.Generic;
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

        public Operation GetMoney(int userId, int amount)
        {
            var operation = new Operation
            {
                OperationType = OperationType.GetMoney,
                PerformTime = DateTime.Now,
                AdditionInformation = amount.ToString()
            };
            var user = _profileRepository.Get(u => u.Id == userId, Strings.Operations);
            operation.Success = amount < user.Balance;
            if (operation.Success)
            {
                user.Balance = user.Balance - amount;
                //Perform operation that withdraws money :))))
            }
            user.Operations.Add(operation);
            _profileRepository.Update(user);
            return operation;
        }

        public User Balance(int userId)
        {
            var operation = new Operation
            {
                OperationType = OperationType.Balance,
                PerformTime = DateTime.Now,
                Success = true
            };
            var user = _profileRepository.Get(u => u.Id == userId, Strings.Operations);

                user.Operations.Add(operation);
                _profileRepository.Update(user);
            return user;
        }
    }
}