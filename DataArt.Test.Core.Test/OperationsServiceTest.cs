using System;
using System.Collections.Generic;
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
        public void Balance_WhenRequested_ThenReturnsUserWithOperations_Test()
        {
            TestHelper.UsingContainer(container =>
            {
                //Assign
                var operationsRepositoryMock = new Mock<IRepository<Operation>>();
                var profileRepository = new Mock<IRepository<User>>();
                profileRepository.Setup(pr => pr.Get(It.IsAny<Func<User, bool>>())).Returns(new User()
                {
                    Balance = 10000,
                    Operations = new List<Operation>
                    {
                        new Operation
                        {
                            OperationType = OperationType.GetMoney,
                            AdditionInformation = "100",
                            PerformTime = DateTime.Now.AddDays(-2)
                        }
                    }
                });
                container.Register(Component.For<IRepository<Operation>>().Instance(operationsRepositoryMock.Object));
                container.Register(Component.For<IRepository<User>>().Instance(profileRepository.Object));
                var service = container.Resolve<IOperationsService>();
                //Act
                var result = service.Balance(6);
                //Assert
                Assert.AreEqual(10000, result.Balance);
                Assert.AreEqual(2, result.Operations.Count);
            });
        }

        [Test]
        public void Balance_WhenAmountLessThenMoney_ThenReturnsTrue_Test()
        {
            TestHelper.UsingContainer(container =>
            {
                //Assign
                var operationsRepositoryMock = new Mock<IRepository<Operation>>();
                var profileRepository = new Mock<IRepository<User>>();
                profileRepository.Setup(pr => pr.Get(It.IsAny<Func<User, bool>>())).Returns(new User()
                {
                    Balance = 10000
                });
                container.Register(Component.For<IRepository<Operation>>().Instance(operationsRepositoryMock.Object));
                container.Register(Component.For<IRepository<User>>().Instance(profileRepository.Object));
                var service = container.Resolve<IOperationsService>();
                //Act
                var result = service.GetMoney(23, 1000);
                //Assert
                Assert.IsTrue(result.Success);
            });
        }

        [Test]
        public void GetMoney_WhenAmountMoreThenMoney_ThenReturnsFalse_Test()
        {
            TestHelper.UsingContainer(container =>
            {
                //Assign
                var operationsRepositoryMock = new Mock<IRepository<Operation>>();
                var profileRepository = new Mock<IRepository<User>>();
                profileRepository.Setup(pr => pr.Get(It.IsAny<Func<User, bool>>())).Returns(new User()
                {
                    Balance = 10000
                });
                container.Register(Component.For<IRepository<Operation>>().Instance(operationsRepositoryMock.Object));
                container.Register(Component.For<IRepository<User>>().Instance(profileRepository.Object));
                var service = container.Resolve<IOperationsService>();
                //Act
                var result = service.GetMoney(23,100000);
                //Assert
                Assert.IsFalse(result.Success);
            });
        }
    }
}