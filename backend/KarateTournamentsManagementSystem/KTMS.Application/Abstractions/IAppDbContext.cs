using Microsoft.EntityFrameworkCore;

namespace KTMS.Application.Abstractions
{
    public interface IAppDbContext
    {
        public DbSet<Enrollment> Enrollments { get; }
        public DbSet<Belt> Belts { get; }
        public DbSet<Bracket> Brackets { get; }
        public DbSet<Category> Categories { get; }
        public DbSet<City> Cities { get; }
        public DbSet<Club> Clubs { get; }
        public DbSet<Coach> Coaches { get; }
        public DbSet<Contestant> Contestants { get; }
        public DbSet<Country> Countries { get; }
        public DbSet<Discipline> Disciplines { get; }
        public DbSet<Gender> Genders { get; }
        public DbSet<Judge> Judges { get; }
        public DbSet<Location> Locations { get; }
        public DbSet<Notification> Notifications { get; }
        public DbSet<Organizer> Organizers { get; }
        public DbSet<Payment> Payments { get; }
        public DbSet<Result> Results { get; }
        public DbSet<Role> Roles { get; }
        public DbSet<Schedule> Schedules { get; }
        public DbSet<Tatami> Tatamis { get; }
        public DbSet<Tournament> Tournaments { get; }
        public DbSet<TournamentContestant> TournamentContestants { get; }
        public DbSet<TournamentJudge> TournamentJudges { get; }
        public DbSet<KTMSUserEntity> Users { get; }
        DbSet<RefreshTokenEntity> RefreshTokens { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
