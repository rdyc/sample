using MediatR;

namespace Sample.Api.Application.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public interface IPersonQueries : IRequest<string>
    {
        Task<dynamic> GetAsync(int id);

        Task<IEnumerable<string>> GetAsync();
    }
}