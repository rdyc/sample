using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Concrete.Repository.Entities
{
    [Table("Person")]
    public class PersonEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}