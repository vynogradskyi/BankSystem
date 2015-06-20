using System;
using System.Collections.Generic;
using System.Linq;
using DataArt.Test.Core.Abstract;
using DataArt.Test.Core.Domain;
using DataArt.Test.DAL.Contexts;

namespace DataArt.Test.DAL.Repository
{
    public class Repository : IProfileRepository
    {
        public void PopulateForTesting(List<User> users)
        {
            using (var ctx = new BankContext())
            {
                foreach (var user in ctx.Users)
                {
                    ctx.Users.Remove(user);
                }
                ctx.Users.AddRange(users);
                ctx.SaveChanges();
            }
        }

        public T Get<T>(Func<T, bool> predicate) where T : class
        {
            using (var ctx = new BankContext())
            {
                return ctx.Set<T>().Where(predicate).FirstOrDefault();
            }
        }

        public bool Exists<T>(Func<T, bool> predicate) where T : class
        {
            using (var ctx = new BankContext())
            {
                var debug = ctx.Set<T>().Any(predicate);
                return ctx.Set<T>().Any(predicate);
            }
        }
    }
}