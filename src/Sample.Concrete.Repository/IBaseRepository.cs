using System.Collections.Generic;
using Sample.Concrete.Repository.Entities;

namespace Sample.Concrete.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IEnumerable<T> Get();

        T Get(long id);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}