using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DataArt.Test.Core.Abstract;
using DataArt.Test.Core.Concrete;
using DataArt.Test.Core.Domain;
using Moq;
using NUnit.Framework;

namespace DataArt.Test.Core.Test
{
    [TestFixture]
    public class AuthenticationSeriveTest
    {

        [Test]
        public void GetUser_WhenCardExist_ThenReturnsUser_Test()
        {
            UsingContainer(container =>
            {
                //Assign
                const string validCardNumber = "1111111111111111";
                var repositoryMock = new Mock<IProfileRepository>();
                repositoryMock.Setup(r => r.Get(It.IsAny<Func<User, bool>>())).Returns(new User { UserName = "John Doe" });
                container.Register(Component.For<IProfileRepository>().Instance(repositoryMock.Object));
                var service = container.Resolve<IAuthenticationService>();
                //Act
                var user = service.GetUser(validCardNumber);
                //Assert
                Assert.AreEqual("John Doe", user.UserName);
            });
        }

        [Test]
        public void GetUser_WhenDoesntCardExist_ThenReturnsNull_Test()
        {
            UsingContainer(container =>
            {
                //Assign
                const string invalidCardNumber = "1111111111111112";
                var repositoryMock = new Mock<IProfileRepository>();
                repositoryMock.Setup(r => r.Get(It.IsAny<Func<User, bool>>())).Returns<User>(null);
                container.Register(Component.For<IProfileRepository>().Instance(repositoryMock.Object));
                var service = container.Resolve<IAuthenticationService>();
                //Act
                var user = service.GetUser(invalidCardNumber);
                //Assert
                Assert.IsNull(user);
            });
        }

        [Test]
        public void CheckCardExist_WhenCardExist_ThenReturnsTrue_Test()
        {
            UsingContainer(container =>
            {
                //Assign
                const string invalidCardNumber = "1111111111111112";
                var repositoryMock = new Mock<IProfileRepository>();
                repositoryMock.Setup(r => r.Exists(It.IsAny<Func<User, bool>>())).Returns(true);
                container.Register(Component.For<IProfileRepository>().Instance(repositoryMock.Object));
                var service = container.Resolve<IAuthenticationService>();
                //Act
                var resp = service.CheckCardExist(invalidCardNumber);
                //Assert
                Assert.IsTrue(resp);
            });
        }

        [Test]
        public void CheckCardExist_WhenCardDoesntExist_ThenReturnsFalse_Test()
        {
            UsingContainer(container =>
            {
                //Assign
                const string validCardNumber = "1111111111111112";
                var repositoryMock = new Mock<IProfileRepository>();
                repositoryMock.Setup(r => r.Exists(It.IsAny<Func<User,bool>>())).Returns(false);
                container.Register(Component.For<IProfileRepository>().Instance(repositoryMock.Object));
                var service = container.Resolve<IAuthenticationService>();
                //Act
                var resp = service.CheckCardExist(validCardNumber);
                //Assert
                Assert.IsFalse(resp);
            });
        }

        [Test]
        public void CheckCardNotBlocked_WhenCardNotBlocked_ThenReturnsTrue_Test()
        {
            UsingContainer(container =>
            {
                //Assign
                const string validCardNumber = "1111111111111112";
                var repositoryMock = new Mock<IProfileRepository>();
                repositoryMock.Setup(r => r.Get(It.IsAny<Func<User, bool>>())).Returns(new User()
                {
                    Blocked = false
                });
                container.Register(Component.For<IProfileRepository>().Instance(repositoryMock.Object));
                var service = container.Resolve<IAuthenticationService>();
                //Act
                var resp = service.CheckCardNotBlocked(validCardNumber);
                //Assert
                Assert.IsTrue(resp);
            });
        }

        [Test]
        public void CheckCardNotBlocked_WhenCardBlocked_ThenReturnsFalse_Test()
        {
            UsingContainer(container =>
            {
                //Assign
                const string validCardNumber = "1111111111111112";
                var repositoryMock = new Mock<IProfileRepository>();
                repositoryMock.Setup(r => r.Get(It.IsAny<Func<User, bool>>())).Returns(new User()
                {
                    Blocked = true
                });
                container.Register(Component.For<IProfileRepository>().Instance(repositoryMock.Object));
                var service = container.Resolve<IAuthenticationService>();
                //Act
                var resp = service.CheckCardNotBlocked(validCardNumber);
                //Assert
                Assert.IsFalse(resp);
            });
        }

        [Test]
        public void CheckPin_WhenPinIsOk_ThenReturnsTrue_Test()
        {
            UsingContainer(container =>
            {
                //Assign
                const string validCardNumber = "1111111111111112";
                const string validPin = "dfasdfsdfasdfsadfsafsadfasd";
                var repositoryMock = new Mock<IProfileRepository>();
                repositoryMock.Setup(r => r.Exists(It.IsAny<Func<User, bool>>())).Returns(true);
                container.Register(Component.For<IProfileRepository>().Instance(repositoryMock.Object));
                var service = container.Resolve<IAuthenticationService>();
                //Act
                var resp = service.CheckPin(validCardNumber, validPin);
                //Assert
                Assert.IsTrue(resp);
            });
        }

        [Test]
        public void CheckPin_WhenPinIsNotOk_ThenReturnsFalse_Test()
        {
            UsingContainer(container =>
            {
                //Assign
                const string validCardNumber = "1111111111111112";
                const string validPin = "dfasdfsdfasdfsadfsafsadfasd";
                var repositoryMock = new Mock<IProfileRepository>();
                repositoryMock.Setup(r => r.Exists(It.IsAny<Func<User, bool>>())).Returns(false);
                container.Register(Component.For<IProfileRepository>().Instance(repositoryMock.Object));
                var service = container.Resolve<IAuthenticationService>();
                //Act
                var resp = service.CheckPin(validCardNumber, validPin);
                //Assert
                Assert.IsFalse(resp);
            });
        }

        public static void UsingContainer(Action<IWindsorContainer> action)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IAuthenticationService>().ImplementedBy<AuthenticationService>());
            action(container);
        }
    }
}