using System;
using System.Collections.Generic;
using DataArt.Test.Core.Domain;

namespace DataArt.Test.Core.Abstract
{
    //todo: change to generic repository
    public interface IProfileRepository
    {
        void PopulateForTesting(List<User> users);
        T Get<T>(Func<T, bool> predicate) where T : class;
        bool Exists<T>(Func<T, bool> predicate) where T : class;
    }
}