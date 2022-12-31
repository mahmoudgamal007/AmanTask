using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmanTaskBackEnd.Entities
{
    public class Address : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }

    }
}
