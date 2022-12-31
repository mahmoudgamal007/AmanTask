using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmanTaskBackEnd.Entities
{
    public class Phone : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression("01[0-2,5][0-9]{8}$")]
        public string PhoneNumber { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
     
    }
}

