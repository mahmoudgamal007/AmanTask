using System.ComponentModel.DataAnnotations;

namespace AmanTaskBackEnd.Entities
{
    public class Department : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

    }
}
