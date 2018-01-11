using AutoMapper;
using Sample.Concrete.Repository.Entities;
using Sample.Object.Domains;

namespace Sample.Concrete.Profiles
{
    public class ConcreteMapperProfile : Profile
    {
        public ConcreteMapperProfile()
        {
            CreateMap<PersonEntity, ArtistDto>().ReverseMap();

            //base.Configure();

            /* Mapper.CreateMap<Lookups, Models.Domains.Lookups>();

            Mapper.CreateMap<Groups, Models.Domains.BenefitGroup>()
                .ForMember(a => a.Code, options => options.MapFrom(source => source.Group_Id))
                .ForMember(a => a.Description, options => options.MapFrom(source => source.Group_Description))
                .ForMember(a => a.Type, options => options.MapFrom(source => new Models.Domains.BenefitType { Id = source.Group_Type_Id }))
                .ForMember(a => a.Administration, options => options.MapFrom(source => new Models.Domains.BenefitAdministration { Id = source.Group_Type_Id }));

            Mapper.CreateMap<Entities.AnnualEnrollment.BenefitGroup, Models.Domains.BenefitGroup>()
                .ForMember(e => e.Code, options => options.MapFrom(source => source.Group_Id))
                .ForMember(e => e.Description, options => options.MapFrom(source => source.Group_Description))
                .ForMember(e => e.Type, options => options.MapFrom(source => new Models.Domains.BenefitType() { Id = source.Group_Type_Id, Description = source.Group_Type_Name }))
                .ForMember(e => e.Administration, options => options.MapFrom(source => new Models.Domains.BenefitAdministration() { Id = source.Group_Administration_Id, Description = source.Group_Administration_Name }));

            Mapper.CreateMap<Entities.CoverageLevel, Models.Domains.CoverageLevel>()
                .ForMember(e => e.Code, options => options.MapFrom(source => source.Coverage_Level_Id))
                .ForMember(e => e.Description, options => options.MapFrom(source => source.Coverage_Level_Description))
                .ForMember(e => e.Order, options => options.MapFrom(source => source.Coverage_Level_Order));

            Mapper.CreateMap<Entities.Benefit, Models.Domains.BenefitPlan>()
                .ForMember(e => e.Id, options => options.MapFrom(source => source.Benefit_Id))
                .ForMember(e => e.Name, options => options.MapFrom(source => source.Benefit_Description))
                .ForMember(e => e.Order, options => options.MapFrom(source => source.Benefit_Order));

            Mapper.CreateMap<Entities.BenefitOption, Models.Domains.BenefitOption>()
                .ForMember(e => e.Id, options => options.MapFrom(source => source.Option_Id))
                .ForMember(e => e.BenefitCode, options => options.MapFrom(source => source.Option_Benefit_Id))
                //.ForMember(e => e.BenefitOrder, options => options.MapFrom(source => source.Option_Order))
                .ForMember(e => e.Code, options => options.MapFrom(source => source.Option_Code))
                .ForMember(e => e.Description, options => options.MapFrom(source => source.Option_Name))
                .ForMember(e => e.Order, options => options.MapFrom(source => source.Option_Order))
                .ForMember(e => e.CoverageLevel, options => options.MapFrom(source => new Models.Domains.CoverageLevel() { Code = source.Option_Coverage_Level_Id }));
                

            Mapper.CreateMap<Entities.AnnualEnrollment.BenefitPlanDetail, Models.Domains.BenefitOption>()
                .ForMember(e => e.Id, options => options.MapFrom(source => source.Option_Id))
                .ForMember(e => e.BenefitCode, options => options.MapFrom(source => source.Benefit_Id))
                .ForMember(e => e.BenefitOrder, options => options.MapFrom(source => source.Benefit_Order))
                .ForMember(e => e.Code, options => options.MapFrom(source => source.Option_Code))
                .ForMember(e => e.Description, options => options.MapFrom(source => source.Option_Name))
                .ForMember(e => e.Order, options => options.MapFrom(source => source.Option_Order))
                .ForMember(e => e.CoverageLevel, options => options.MapFrom(source =>
                    new Models.Domains.CoverageLevel()
                    {
                        Code = source.Coverage_Level_Id,
                        Description = source.Coverage_Level_Description,
                        Order = source.Coverage_Level_Order
                    }));

            Mapper.CreateMap<Entities.AnnualEnrollment.BenefitPlanCostDetail, Models.Domains.BenefitOptionCost>()
                .ForMember(e => e.Id, options => options.MapFrom(source => source.Option_Id))
                .ForMember(e => e.BenefitCode, options => options.MapFrom(source => source.Benefit_Id))
                .ForMember(e => e.BenefitOrder, options => options.MapFrom(source => source.Benefit_Order))
                .ForMember(e => e.Code, options => options.MapFrom(source => source.Option_Code))
                .ForMember(e => e.Description, options => options.MapFrom(source => source.Option_Name))
                .ForMember(e => e.Order, options => options.MapFrom(source => source.Option_Order))
                .ForMember(e => e.CoverageAmount, options => options.MapFrom(source => source.Coverage_Amount))
                .ForMember(e => e.AnnualCost, options => options.MapFrom(source => source.Annual_Cost))
                .ForMember(e => e.PerPayCost, options => options.MapFrom(source => source.Per_Pay_Cost))
                .ForMember(e => e.IsDefault, options => options.MapFrom(source => source.IsDefault))
                .ForMember(e => e.IsNOI, options => options.MapFrom(source => source.IsNOI))
                .ForMember(e => e.CoverageLevel, options => options.MapFrom(source =>
                    new Models.Domains.CoverageLevel()
                    {
                        Code = source.Coverage_Level_Id,
                        Description = source.Coverage_Level_Description,
                        Order = source.Coverage_Level_Order
                    }));


            Mapper.CreateMap<Entities.PageTemplate, Models.Domains.PageTemplate>();
            Mapper.CreateMap<Entities.PageAttribute, Models.Domains.PageAttribute>();
            Mapper.CreateMap<Entities.UserRole, Models.Domains.UserRole>();

            #region Update
            Mapper.CreateMap<Models.Domains.BenefitGroupUpdate, Groups>()
                //.ForMember(a => a.Group_Id, options => options.MapFrom(source => source.Code))
                .ForMember(a => a.Group_Description, options => options.MapFrom(source => source.Description))
                .ForMember(a => a.Group_Type_Id, options => options.MapFrom(source => source.Type_Id))
                .ForMember(a => a.Group_Administration_Id, options => options.MapFrom(source => source.Administration_Id));

            Mapper.CreateMap<Models.Domains.CoverageLevel, CoverageLevel>()
                .ForMember(a => a.Coverage_Level_Id, options => options.MapFrom(source => source.Code))
                .ForMember(a => a.Coverage_Level_Description, options => options.MapFrom(source => source.Description))
                .ForMember(a => a.Coverage_Level_Order, options => options.MapFrom(source => source.Order));

            Mapper.CreateMap<Models.Domains.BenefitPlan, Benefit>()
                .ForMember(a => a.Benefit_Id, options => options.MapFrom(source => source.Id))
                .ForMember(a => a.Benefit_Description, options => options.MapFrom(source => source.Name))
                .ForMember(a => a.Benefit_Order, options => options.MapFrom(source => source.Order));

            Mapper.CreateMap<Models.Domains.BenefitOptionUpdate, BenefitOption>()
                .ForMember(a => a.Option_Id, options => options.MapFrom(source => source.Id))
                .ForMember(a => a.Option_Code, options => options.MapFrom(source => source.Code))
                .ForMember(a => a.Option_Name, options => options.MapFrom(source => source.Description))
                .ForMember(a => a.Option_Order, options => options.MapFrom(source => source.Order))
                .ForMember(a => a.Option_Benefit_Id, options => options.MapFrom(source => source.BenefitCode))
                .ForMember(a => a.Option_Coverage_Level_Id, options => options.MapFrom(source => source.CoverageLevelId));
            #endregion
            
            #region Add
            Mapper.CreateMap<Models.Domains.BenefitOptionAdd, BenefitOption>()
                .ForMember(a => a.Option_Code, options => options.MapFrom(source => source.Code))
                .ForMember(a => a.Option_Name, options => options.MapFrom(source => source.Description))
                .ForMember(a => a.Option_Order, options => options.MapFrom(source => source.Order))
                .ForMember(a => a.Option_Benefit_Id, options => options.MapFrom(source => source.BenefitCode))
                .ForMember(a => a.Option_Coverage_Level_Id, options => options.MapFrom(source => source.CoverageLevelId));

            Mapper.CreateMap<Models.Domains.LookupsAdd, Lookups>();
            Mapper.CreateMap<Models.Domains.PageTemplate, Entities.PageTemplate>();
            Mapper.CreateMap<Models.Domains.PageAttribute, Entities.PageAttribute>();
            Mapper.CreateMap<Models.Domains.UserRole, Entities.UserRole>();
            #endregion */

        }
    }
}
