using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using DataArt.Test.Core.Abstract;
using DataArt.Test.Core.Domain;
using DataArt.Test.DAL.Contexts;

namespace DataArt.Test.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IHaveId
    {

        public T Get(Func<T, bool> predicate, params string[] parameters)
        {
            using (var ctx = new BankContext())
            {
                var res = ctx.Set<T>();
                foreach (var p in parameters)
                {
                    res.Include(p);
                }
                return res.Where(predicate).FirstOrDefault();
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
                return ctx.Set<T>().Any(predicate);
            }
        }

        public void Add(T entity)
        {

            using (var ctx = new BankContext())
            {
                ctx.Set<T>().Add(entity);
                ctx.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            using (var ctx = new BankContext())
            {
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.Entry(entity).CurrentValues.SetValues(entity);
                ctx.SaveChanges();
            }
        }
    }
}