using System;
using System.Collections.Generic;

namespace DataArt.Test.Core.Abstract
{
    //todo: change to generic repository
    public interface IRepository<T> where T : class,IHaveId
    {
        T Get(Func<T, bool> predicate, params string[] parameters);
        IEnumerable<T> Find(Func<T, bool> predicate);
        bool Exists(Func<T, bool> predicate);
        void Add(T enity);
        void Update(T entity);
    }
}