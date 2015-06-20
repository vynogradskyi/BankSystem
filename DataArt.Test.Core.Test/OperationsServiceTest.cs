using Castle.MicroKernel.Registration;
using DataArt.Test.Core.Abstract;
using DataArt.Test.Core.Domain;
using Moq;
using NUnit.Framework;

namespace DataArt.Test.Core.Test
{
    [TestFixture]
    public class OperationsServiceTest
    {

        [Test]
        public void Balance_WhenAmountLessThenMoney_ThenReturnsTrue_Test()
        {
            TestHelper.UsingContainer(container =>
            {
                //Assign
                var operationsRepositoryMock = new Mock<IRepository<Operation>>();
                operationsRepositoryMock.Setup(r => r.)
                container.Register(Component.For<IRepository<Operation>>().Instance(operationsRepositoryMock.Object));
                //Act

                //Assert
            });
        }

        [Test]
        public void Balance_WhenAmountMoreThenMoney_ThenReturnsFalse_Test()
        {
            TestHelper.UsingContainer(container =>
            {
                //Assign

                //Act

                //Assert
            });
        }
    }
}