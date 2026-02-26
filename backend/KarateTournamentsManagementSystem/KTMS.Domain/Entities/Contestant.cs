using KTMS.Domain.Entities.Identity;

namespace KTMS.Domain.Entities
{
    public class Contestant
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BeltId { get; set; }
        public int ClubId { get; set; }

        //Navigation Properties
        public KTMSUserEntity User { get; set; }
        public Belt Belt { get; set; }
        public Club Club { get; set; }

        //Collections
        public ICollection<Bracket> Brackets { get; set; } = new List<Bracket>();
        public ICollection<Result> Results { get; set; } = new List<Result>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<TournamentContestant> TournamentContestants { get; set; } = new List<TournamentContestant>();
    }
}
