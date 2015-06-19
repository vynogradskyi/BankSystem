using System;
using System.Net;
using DataArt.Test.Core.Domain;

namespace DataArt.Test.Core.Abstract
{
    public interface IProfileRepository
    {
        User Get<T>(Func<T, bool> predicate);
        bool Exists(Func<User, bool> predicate);
    }
}