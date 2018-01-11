using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Sample.Concrete.Services;

namespace Sample.Api.Application.Queries
{
    public class PersonQueries : IPersonQueries
    {
        private string _connectionString = string.Empty;

        public PersonQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }
        
        public Task<dynamic> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<string>> GetAsync()
        {
            var data = new[]
            {
                "dsada"
            };
            return  Task.FromResult(data.AsEnumerable());
        }
    }
}