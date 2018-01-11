using System.Collections.Generic;
using Sample.Object.Domains;

namespace Sample.Contract.Services
{
    public interface IBaseService<T> where T : BaseDto
    {
        IEnumerable<T> Get();

        T Get(long id);

        void Create(T model);

        void Update(T model);

        void Delete(T model);
    }
}