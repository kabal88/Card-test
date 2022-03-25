using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IRepository<T> where T : class
    {
        void RegisterObject(T obj);
        void UnRegisterObject(T obj);
        IEnumerable<T> GetObjectByPredicate(Func<T, bool> predicate);
        bool TryGetObject(out T obj);
    }
}