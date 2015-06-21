using System;
using System.Data.Entity;
using DataArt.Test.Core.Concrete;
using DataArt.Test.Core.Domain;

namespace DataArt.Test.DAL.Contexts
{
    public class BankContextInitializer : DropCreateDatabaseAlways<BankContext>
    {
        protected override void Seed(BankContext context)
        {
            var users = MockData.GetUsers();
            foreach (var user in users)
            {
                user.Operations.Add(
                        new Operation
                        {
                            OperationType = OperationType.Balance,
                            PerformTime = DateTime.Now.AddDays(-10)
                        }
                );

                user.Operations.Add(
                        new Operation
                        {
                            OperationType = OperationType.GetMoney,
                            PerformTime = DateTime.Now.AddDays(-5),
                            AdditionInformation = "800"
                        }
                );

                context.Set<User>().Add(user);
            }
            base.Seed(context);
        }
    }
}