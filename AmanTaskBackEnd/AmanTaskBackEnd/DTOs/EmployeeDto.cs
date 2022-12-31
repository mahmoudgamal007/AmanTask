namespace AmanTaskBackEnd.DTOs
{
    public class EmployeeDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        
    }
}
