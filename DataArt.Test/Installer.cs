using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DataArt.Test.Core.Abstract;
using DataArt.Test.Core.Concrete;
using DataArt.Test.Core.Domain;
using DataArt.Test.DAL.Repository;

namespace DataArt.Test
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient(),
                Classes.FromAssemblyContaining<AccountService>()
                    .InSameNamespaceAs<AccountService>()
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient(),
                Component.For<IRepository<User>>().ImplementedBy<Repository<User>>());
        }

    }
}