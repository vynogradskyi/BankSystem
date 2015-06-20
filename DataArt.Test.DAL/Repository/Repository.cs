using System;
using System.Collections.Generic;
using System.Linq;
using DataArt.Test.Core.Abstract;
using DataArt.Test.Core.Domain;
using DataArt.Test.DAL.Contexts;

namespace DataArt.Test.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public void PopulateUsersForTesting(List<User> users)
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

        public T Get(Func<T, bool> predicate)
        {
            using (var ctx = new BankContext())
            {
                return ctx.Set<T>().Where(predicate).FirstOrDefault();
            }
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            using (var ctx = new BankContext())
            {
                return ctx.Set<T>().Where(predicate);
            }
        }

        public bool Exists(Func<T, bool> predicate) 
        {
            using (var ctx = new BankContext())
            {
                var debug = ctx.Set<T>().Any(predicate);
                return ctx.Set<T>().Any(predicate);
            }
        }

        public void Add(T enity)
        {
            
            
        }
    }
}