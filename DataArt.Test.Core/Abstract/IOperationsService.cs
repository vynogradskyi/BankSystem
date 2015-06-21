using DataArt.Test.Core.Domain;

namespace DataArt.Test.Core.Abstract
{
    public interface IOperationsService
    {
        bool GetMoney(int userId, int amount);
        User Balance(int userId);

    }
}