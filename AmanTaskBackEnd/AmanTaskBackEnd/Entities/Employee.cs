using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmanTaskBackEnd.Entities
{
    public class Employee : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
       
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public virtual List<Address> Addresses { get; set; }
        public virtual List<Phone> Phones { get; set; }

    }
}
