namespace KTMS.Application.Modules.Clubs.Dtos
{
    public class CreateClubDto
    {
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
