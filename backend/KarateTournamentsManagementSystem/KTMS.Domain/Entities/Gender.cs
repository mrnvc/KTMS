using KTMS.Domain.Entities.Identity;

namespace KTMS.Domain.Entities
{
    public class Gender
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        //Collections
        public ICollection<KTMSUserEntity> Users { get; set; } = new List<KTMSUserEntity>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
