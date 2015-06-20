using System.Collections.Generic;
using DataArt.Test.Core.Abstract;
using DataArt.Test.Core.Domain;

namespace DataArt.Test.Core.Concrete
{
    public class OperationsService : IOperationsService
    {
        private readonly IRepository<User> _repository;

        public OperationsService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public bool GetMoney(int amount)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Operation> GetOperations(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}