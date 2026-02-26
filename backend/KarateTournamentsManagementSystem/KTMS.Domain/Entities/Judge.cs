using KTMS.Domain.Entities.Identity;

namespace KTMS.Domain.Entities
{
    public class Judge
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string License { get; set; }
        public string? Rank { get; set; }

        //Navigation Properties
        public KTMSUserEntity User { get; set; }

        //Collections
        public ICollection<Tatami> Tatamis { get; set; } = new List<Tatami>();
        public ICollection<TournamentJudge> TournamentJudges { get; set; } = new List<TournamentJudge>();
    }
}
