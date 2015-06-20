using System.Collections.Generic;
using DataArt.Test.Core.Domain;

namespace DataArt.Test.Core.Abstract
{
    public interface IOperationsService
    {
        bool GetMoney(int amount);
        IEnumerable<Operation> GetOperations(int userId);

    }
}