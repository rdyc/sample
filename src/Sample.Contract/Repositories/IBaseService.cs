
using System;
using System.Collections.Generic;

namespace Sample.Contract.Repositories
{
    public interface IBaseService<T>
    {
        IEnumerable<T> Get();

        T Get(int id);

        void Create(T model);

        void Update(int id, T model);

        void Delete(int id);
    }
}