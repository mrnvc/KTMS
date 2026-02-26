using KTMS.Domain.Entities.Identity;

namespace KTMS.Domain.Entities
{
    public class Organizer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TournamentId { get; set; }
        public required string Role { get; set; }
        public string? ObligationDescription { get; set; }   

        //Navigation Properties
        public KTMSUserEntity User { get; set; }
        public Tournament Tournament { get; set; }
    }
}
