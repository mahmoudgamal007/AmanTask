namespace AmanTaskBackEnd.DTOs
{
    public class AddressDto
    {
        public int? Id { get; set; }
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int EmployeeId { get; set; }
    }
}
