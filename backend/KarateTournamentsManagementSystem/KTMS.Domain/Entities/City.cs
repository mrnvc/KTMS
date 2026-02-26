using KTMS.Domain.Entities.Identity;

namespace KTMS.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public required string Name { get; set; }

        //Navigation Properties
        public Country Country { get; set; }

        //collections
        public ICollection<KTMSUserEntity> Users { get; set; } = new List<KTMSUserEntity>();
        public ICollection<Club> Clubs { get; set; } = new List<Club>();
        public ICollection<Location> Locations { get; set; } = new List<Location>();
    }
}
