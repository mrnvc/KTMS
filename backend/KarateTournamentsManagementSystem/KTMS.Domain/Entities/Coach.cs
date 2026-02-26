using KTMS.Domain.Entities.Identity;

namespace KTMS.Domain.Entities
{
    public class Coach
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public int UserId { get; set; }
        public int BeltId { get; set; }
        public string? LicenseNumber { get; set; }
        public string? CertificationLevel { get; set; }
        public bool? Verified { get; set; }

        //Navigation Properties
        public KTMSUserEntity User { get; set; }
        public Club Club { get; set; }
        public Belt Belt { get; set; }
    }
}
