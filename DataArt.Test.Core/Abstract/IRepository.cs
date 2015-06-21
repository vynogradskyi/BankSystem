using System;
using System.Collections.Generic;
using DataArt.Test.Core.Domain;

namespace DataArt.Test.Core.Abstract
{
    //todo: change to generic repository
    public interface IRepository<T> where T : class,IHaveId
    {
        void PopulateUsersForTesting(List<User> users);
        T Get(Func<T, bool> predicate);
        IEnumerable<T> Find(Func<T, bool> predicate);
        bool Exists(Func<T, bool> predicate);
        void Add(T enity);
        void Update(T entity);
    }
}