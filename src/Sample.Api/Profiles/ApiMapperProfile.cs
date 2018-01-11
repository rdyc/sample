using AutoMapper;
using Sample.Api.Models;
using Sample.Api.Models.Requests;
//using Sample.Object.Domains;

namespace Sample.Api.Profiles
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            // CreateMap<ArtistDto, ArtistModel>()
            //     .ForMember(a => a.id, options => options.MapFrom(source => source.Id))
            //     .ForMember(a => a.firstName, options => options.MapFrom(source => source.FirstName + ' '+ source.LastName))
            //     .ForMember(a => a.email, options => options.MapFrom(source => source.Email));


            // CreateMap<ArtistPostModel, ArtistDto>()
            //     .ForMember(a => a.FirstName, options => options.MapFrom(source => source.firstName))
            //     .ForMember(a => a.Email, options => options.MapFrom(source => source.email));
        }
    }
}