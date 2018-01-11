
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Concrete.Repository.Entities;

namespace Sample.Concrete.Repository.Data
{
    public class PersonMap
    {
        public PersonMap(EntityTypeBuilder<PersonEntity> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.FirstName).IsRequired();
            entityBuilder.Property(t => t.LastName).IsRequired();
            entityBuilder.Property(t => t.Email).IsRequired();          
        }
    }
}