using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DataArt.Test.Core.Abstract;
using DataArt.Test.Core.Concrete;

namespace DataArt.Test.Core.Test
{
    public static class TestHelper
    {
        public static void UsingContainer(Action<IWindsorContainer> action)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IAccountService>().ImplementedBy<AccountService>());
            container.Register(Component.For<IOperationsService>().ImplementedBy<OperationsService>());
            action(container);
        }
    }
}