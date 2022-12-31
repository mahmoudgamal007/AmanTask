using System.ComponentModel.DataAnnotations;

namespace AmanTaskBackEnd.DTOs
{
    public class PhoneDto
    {
        public int? Id { get; set; }
        [RegularExpression("01[0-2,5][0-9]{8}$")]
        public string PhoneNumber { get; set; }
        public int EmployeeId { get; set; }
    }
}
