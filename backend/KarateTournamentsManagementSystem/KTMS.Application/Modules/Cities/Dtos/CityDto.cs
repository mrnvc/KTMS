namespace KTMS.Application.Modules.Cities.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        public required string CityName { get; set; }
        public required string Country { get; set; }
    }
}
