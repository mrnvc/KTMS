using KTMS.Application.Abstractions;
using KTMS.Domain.Entities;
using KTMS.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace KTMS.Infrastructure.Database
{
    public partial class DatabaseContext : DbContext, IAppDbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Belt> Belts { get; set; }
        public DbSet<Bracket> Brackets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Contestant> Contestants { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Judge> Judges { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Tatami> Tatamis { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentContestant> TournamentContestants { get; set; }
        public DbSet<TournamentJudge> TournamentJudges { get; set; }
        public DbSet<KTMSUserEntity> Users { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        private readonly TimeProvider _clock;
        public DatabaseContext(DbContextOptions<DatabaseContext> options, TimeProvider clock) : base(options)
        {
            _clock = clock;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ====================
            // ENROLLMENTS
            // ====================
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Contestant)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.ContestantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                 .HasOne(e => e.Tournament)
                 .WithMany(t => t.Enrollments)
                 .HasForeignKey(e => e.TournamentId)
                 .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Discipline)
                .WithMany(d => d.Enrollments)
                .HasForeignKey(e => e.DisciplineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasMany(e => e.Payments)
                .WithOne(p => p.Enrollment)
                .HasForeignKey(p => p.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);


            // ====================
            // BELTS
            // ====================
            modelBuilder.Entity<Belt>()
                .HasMany(b => b.Coaches)
                .WithOne(c => c.Belt)
                .HasForeignKey(c => c.BeltId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Belt>()
                .HasMany(b => b.Contestants)
                .WithOne(c => c.Belt)
                .HasForeignKey(c => c.BeltId)
                .OnDelete(DeleteBehavior.Restrict);


            // ====================
            // BRACKETS
            // ====================
            modelBuilder.Entity<Bracket>()
                .HasOne(b => b.Tournament)
                .WithMany(t => t.Brackets)
                .HasForeignKey(b => b.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bracket>()
                .HasOne(b => b.Tatami)
                .WithMany(t => t.Brackets)
                .HasForeignKey(b => b.TatamiId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bracket>()
                .HasOne(b => b.ContestantOne)
                .WithMany(c => c.Brackets)
                .HasForeignKey(b => b.ContestantOneId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bracket>()
                .HasOne(b => b.ContestantTwo)
                .WithMany()
                .HasForeignKey(b => b.ContestantTwoId)
                .OnDelete(DeleteBehavior.Restrict);


            // ====================
            // CATEGORIES
            // ====================
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Gender)
                .WithMany(g => g.Categories)
                .HasForeignKey(c => c.GenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.Discipline)
                .WithMany(d => d.Categories)
                .HasForeignKey(c => c.DisciplineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Results)
                .WithOne(r => r.Category)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Schedules)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Enrollments)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            // ====================
            // CITIES
            // ====================
            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(co => co.Cities)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<City>()
                .HasMany(c => c.Users)
                .WithOne(u => u.City)
                .HasForeignKey(u => u.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<City>()
                .HasMany(c => c.Clubs)
                .WithOne(cl => cl.City)
                .HasForeignKey(cl => cl.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<City>()
                .HasMany(c => c.Locations)
                .WithOne(l => l.City)
                .HasForeignKey(l => l.CityId)
                .OnDelete(DeleteBehavior.Restrict);


            // ====================
            // CLUBS
            // ====================
            modelBuilder.Entity<Club>()
                .HasOne(cl => cl.City)
                .WithMany(c => c.Clubs)
                .HasForeignKey(cl => cl.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Club>()
                .HasOne(cl => cl.Country)
                .WithMany(c => c.Clubs)
                .HasForeignKey(cl => cl.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Club>()
                .HasMany(cl => cl.Contestants)
                .WithOne(c => c.Club)
                .HasForeignKey(c => c.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Club>()
                .HasMany(cl => cl.Coaches)
                .WithOne(c => c.Club)
                .HasForeignKey(c => c.ClubId)
                .OnDelete(DeleteBehavior.Restrict);


            // ====================
            // COACHES
            // ====================
            modelBuilder.Entity<Coach>()
                .HasOne(c => c.User)
                .WithMany(u => u.Coaches)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Coach>()
                .HasOne(c => c.Club)
                .WithMany(cl => cl.Coaches)
                .HasForeignKey(c => c.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Coach>()
                .HasOne(c => c.Belt)
                .WithMany(b => b.Coaches)
                .HasForeignKey(c => c.BeltId)
                .OnDelete(DeleteBehavior.Restrict);


            // ====================
            // CONTESTANTS
            // ====================
            modelBuilder.Entity<Contestant>()
                .HasOne(c => c.User)
                .WithMany(u => u.Contestants)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contestant>()
                .HasOne(c => c.Club)
                .WithMany(cl => cl.Contestants)
                .HasForeignKey(c => c.ClubId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contestant>()
                .HasOne(c => c.Belt)
                .WithMany(b => b.Contestants)
                .HasForeignKey(c => c.BeltId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contestant>()
                .HasMany(c => c.Brackets)
                .WithOne(b => b.ContestantOne)
                .HasForeignKey(b => b.ContestantOneId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contestant>()
                .HasMany(c => c.Results)
                .WithOne(r => r.Contestant)
                .HasForeignKey(r => r.ContestantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contestant>()
                .HasMany(c => c.Enrollments)
                .WithOne(e => e.Contestant)
                .HasForeignKey(e => e.ContestantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contestant>()
                .HasMany(c => c.TournamentContestants)
                .WithOne(tc => tc.Contestant)
                .HasForeignKey(tc => tc.ContestantId)
                .OnDelete(DeleteBehavior.Cascade);


            // ====================
            // COUNTRIES
            // ====================
            modelBuilder.Entity<Country>()
                .HasMany(c => c.Cities)
                .WithOne(ci => ci.Country)
                .HasForeignKey(ci => ci.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Locations)
                .WithOne(l => l.Country)
                .HasForeignKey(l => l.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Clubs)
                .WithOne(cl => cl.Country)
                .HasForeignKey(cl => cl.CountryId)
                .OnDelete(DeleteBehavior.Restrict);


            // ====================
            // DISCIPLINES
            // ====================
            modelBuilder.Entity<Discipline>()
                .HasMany(d => d.Categories)
                .WithOne(c => c.Discipline)
                .HasForeignKey(c => c.DisciplineId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Discipline>()
                .HasMany(d => d.Enrollments)
                .WithOne(e => e.Discipline)
                .HasForeignKey(e => e.DisciplineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Discipline>()
                .HasMany(d => d.Results)
                .WithOne(r => r.Discipline)
                .HasForeignKey(r => r.DisciplineId)
                .OnDelete(DeleteBehavior.Restrict);


            // ====================
            // GENDERS
            // ====================
            modelBuilder.Entity<Gender>()
                .HasMany(g => g.Users)
                .WithOne(u => u.Gender)
                .HasForeignKey(u => u.GenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Gender>()
                .HasMany(g => g.Categories)
                .WithOne(c => c.Gender)
                .HasForeignKey(c => c.GenderId)
                .OnDelete(DeleteBehavior.Restrict);


            // ====================
            // JUDGES
            // ====================
            modelBuilder.Entity<Judge>()
                .HasOne(j => j.User)
                .WithMany(u => u.Judges)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Judge>()
                 .HasMany(j => j.TournamentJudges)
                 .WithOne(tj => tj.Judge)
                 .HasForeignKey(tj => tj.JudgeId)
                 .OnDelete(DeleteBehavior.Cascade);


            // ====================
            // LOCATIONS
            // ====================
            modelBuilder.Entity<Location>()
                .HasOne(l => l.City)
                .WithMany(c => c.Locations)
                .HasForeignKey(l => l.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Location>()
                .HasOne(l => l.Country)
                .WithMany(c => c.Locations)
                .HasForeignKey(l => l.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Tournaments)
                .WithOne(t => t.Location)
                .HasForeignKey(t => t.LocationId)
                .OnDelete(DeleteBehavior.Restrict);


            // ====================
            // NOTIFICATIONS
            // ====================
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Tournament)
                .WithMany(t => t.Notifications)
                .HasForeignKey(n => n.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);


            // ====================
            // ORGANIZERS
            // ====================
            modelBuilder.Entity<Organizer>()
                .HasOne(o => o.User)
                .WithMany(u => u.Organizers)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Organizer>()
                .HasOne(o => o.Tournament)
                .WithMany(t => t.Organizers)
                .HasForeignKey(o => o.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);


            // ====================
            // PAYMENTS
            // ====================
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Enrollment)
                .WithMany(e => e.Payments)
                .HasForeignKey(p => p.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);


            // ====================
            // RESULTS
            // ====================
            modelBuilder.Entity<Result>()
                .HasOne(r => r.Contestant)
                .WithMany(c => c.Results)
                .HasForeignKey(r => r.ContestantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Discipline)
                .WithMany(d => d.Results)
                .HasForeignKey(r => r.DisciplineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Results)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Tournament)
                .WithMany(t => t.Results)
                .HasForeignKey(r => r.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);


            // ====================
            // ROLES
            // ====================
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);


            // ====================
            // SCHEDULES
            // ====================
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Tournament)
                .WithMany(t => t.Schedules)
                .HasForeignKey(s => s.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Schedules)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Tatami)
                .WithMany(t => t.Schedules)
                .HasForeignKey(s => s.TatamiId)
                .OnDelete(DeleteBehavior.Restrict);


            // ====================
            // TATAMIS
            // ====================
            modelBuilder.Entity<Tatami>()
                .HasOne(t => t.Tournament)
                .WithMany(tr => tr.Tatamis)
                .HasForeignKey(t => t.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tatami>()
                .HasOne(t => t.Judge)
                .WithMany(j => j.Tatamis)
                .HasForeignKey(t => t.JudgeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tatami>()
                .HasMany(t => t.Brackets)
                .WithOne(b => b.Tatami)
                .HasForeignKey(b => b.TatamiId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tatami>()
                .HasMany(t => t.Schedules)
                .WithOne(s => s.Tatami)
                .HasForeignKey(s => s.TatamiId)
                .OnDelete(DeleteBehavior.Restrict);


            // ====================
            // TOURNAMENTS
            // ====================
            modelBuilder.Entity<Tournament>()
                .HasOne(t => t.Location)
                .WithMany(l => l.Tournaments)
                .HasForeignKey(t => t.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Organizers)
                .WithOne(o => o.Tournament)
                .HasForeignKey(o => o.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.TournamentJudges)
                .WithOne(tj => tj.Tournament)
                .HasForeignKey(tj => tj.TournamentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Enrollments)
                .WithOne(e => e.Tournament)
                .HasForeignKey(e => e.TournamentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Notifications)
                .WithOne(n => n.Tournament)
                .HasForeignKey(n => n.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Brackets)
                .WithOne(b => b.Tournament)
                .HasForeignKey(b => b.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Results)
                .WithOne(r => r.Tournament)
                .HasForeignKey(r => r.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Tatamis)
                .WithOne(tt => tt.Tournament)
                .HasForeignKey(tt => tt.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Schedules)
                .WithOne(s => s.Tournament)
                .HasForeignKey(s => s.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.TournamentContestants)
                .WithOne(tc => tc.Tournament)
                .HasForeignKey(tc => tc.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);


            // ====================
            // TOURNAMENT CONTESTANTS
            // ====================
            modelBuilder.Entity<TournamentContestant>()
                .HasOne(tc => tc.Tournament)
                .WithMany(t => t.TournamentContestants)
                .HasForeignKey(tc => tc.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TournamentContestant>()
                .HasOne(tc => tc.Contestant)
                .WithMany(c => c.TournamentContestants)
                .HasForeignKey(tc => tc.ContestantId)
                .OnDelete(DeleteBehavior.Cascade);


            // ====================
            // TOURNAMENT JUDGES
            // ====================
            modelBuilder.Entity<TournamentJudge>()
                .HasOne(tj => tj.Tournament)
                .WithMany(t => t.TournamentJudges)
                .HasForeignKey(tj => tj.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TournamentJudge>()
                .HasOne(tj => tj.Judge)
                .WithMany(j => j.TournamentJudges)
                .HasForeignKey(tj => tj.JudgeId)
                .OnDelete(DeleteBehavior.Cascade);
            

            // ====================
            // USERS
            // ====================
            modelBuilder.Entity<KTMSUserEntity>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KTMSUserEntity>()
                .HasOne(u => u.City)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KTMSUserEntity>()
                .HasOne(u => u.Gender)
                .WithMany(g => g.Users)
                .HasForeignKey(u => u.GenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KTMSUserEntity>()
                .HasMany(u => u.Contestants)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KTMSUserEntity>()
                .HasMany(u => u.Coaches)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KTMSUserEntity>()
                .HasMany(u => u.Judges)
                .WithOne(j => j.User)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KTMSUserEntity>()
                .HasMany(u => u.Organizers)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>().ToTable("Enrollments");
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
            configurationBuilder.Properties<decimal?>().HavePrecision(18, 2);
        }
    }
}
