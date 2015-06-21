using System.Data.Entity;
using DataArt.Test.Core.Domain;

namespace DataArt.Test.DAL.Contexts
{
    public class BankContext : DbContext
    {
        public BankContext():base("Name=ConnectionString1")
        {
            Database.SetInitializer(new BankContextInitializer());
        }

        public DbSet<User> Users { get; set; }
    }
}