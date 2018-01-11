using System;
using System.Collections.Generic;
using AutoMapper;
using Sample.Concrete.Repository;
using Sample.Concrete.Repository.Entities;
using Sample.Contract.Services;
using Sample.Object.Domains;

namespace Sample.Concrete.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IBaseRepository<PersonEntity> _person;

        public ArtistService(IBaseRepository<PersonEntity> person){
            _person = person;
        }
        
        public IEnumerable<ArtistDto> Get()
        {
            return Mapper.Map<IEnumerable<ArtistDto>>(_person.Get());
        }

        public ArtistDto Get(long id)
        {
            return Mapper.Map<ArtistDto>(_person.Get(id));
        }

        public void Create(ArtistDto model)
        {
            _person.Create(Mapper.Map<PersonEntity>(model));
        }

        public void Delete(ArtistDto model)
        {
            throw new NotImplementedException();
        }

        public void Update(ArtistDto model)
        {
            throw new NotImplementedException();
        }
    }
}
