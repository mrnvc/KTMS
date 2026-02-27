using KTMS.Domain.Entities;

namespace KTMS.Infrastructure.Database.Seeders
{
    /// <summary>
    /// Dynamic seeder koji se pokreće u runtime-u (Program.cs).
    /// Koristi se za demo/test podatke koji NISU dio migracija.
    /// </summary>
    public static class DynamicDataSeeder
    {
        public static async Task SeedAsync(DatabaseContext context)
        {
            // Osiguraj da baza postoji (bez migracija)
            await context.Database.EnsureCreatedAsync();

            // Redoslijed je bitan zbog FK veza:
            await SeedRolesAsync(context);
            await SeedGendersAsync(context);
            await SeedCountriesAsync(context);
            await SeedCitiesAsync(context);
            await SeedBeltsAsync(context);
            await SeedDisciplinesAsync(context);

            await SeedClubsAsync(context);
            await SeedLocationsAsync(context);

            await SeedUsersAsync(context);
            await SeedCoachesAsync(context);
            await SeedContestantsAsync(context);
            await SeedJudgesAsync(context);

            await SeedTournamentsAsync(context);
            await SeedTournamentJudgesAsync(context);
            await SeedTatamisAsync(context);
            await SeedCategoriesAsync(context);
            await SeedTournamentContestantsAsync(context);
            await SeedEnrollmentsAsync(context);
            await SeedPaymentsAsync(context);
            await SeedSchedulesAsync(context);
            await SeedBracketsAsync(context);
            await SeedResultsAsync(context);
            await SeedNotificationsAsync(context);
        }

        #region Lookup seeding (Roles, Genders, Countries, Cities, Belts, Disciplines)

        private static async Task SeedRolesAsync(DatabaseContext context)
        {
            if (await context.Roles.AnyAsync())
            {
                Console.WriteLine("⏭️  Roles already exist. Skipping seed.");
                return;
            }

            var roles = new List<Role>
            {
                new Role { Title = "Admin",       Description = "System administrator", Status = true },
                new Role { Title = "Organizer",   Description = "Tournament organizer", Status = true },
                new Role { Title = "Coach",       Description = "Club coach", Status = true },
                new Role { Title = "Judge",       Description = "Tournament judge", Status = true },
                new Role { Title = "Contestant",  Description = "Athlete/contestant", Status = true }
            };

            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {roles.Count} roles added.");
        }

        private static async Task SeedGendersAsync(DatabaseContext context)
        {
            if (await context.Genders.AnyAsync())
            {
                Console.WriteLine("⏭️  Genders already exist. Skipping seed.");
                return;
            }

            var genders = new[]
            {
                new Gender { Name = "Male"  },
                new Gender { Name = "Female"}
            };

            context.Genders.AddRange(genders);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {genders.Length} genders added.");
        }

        private static async Task SeedCountriesAsync(DatabaseContext context)
        {
            if (await context.Countries.AnyAsync())
            {
                Console.WriteLine("⏭️  Countries already exist. Skipping seed.");
                return;
            }

            var countries = new List<Country>
            {
                new Country { Name = "Bosnia and Herzegovina" },
                new Country { Name = "Croatia" },
                new Country { Name = "Serbia" }
            };

            context.Countries.AddRange(countries);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {countries.Count} countries added.");
        }

        private static async Task SeedCitiesAsync(DatabaseContext context)
        {
            if (await context.Cities.AnyAsync())
            {
                Console.WriteLine("⏭️  Cities already exist. Skipping seed.");
                return;
            }

            var bosnia = await context.Countries.FirstAsync(c => c.Name == "Bosnia and Herzegovina");
            var croatia = await context.Countries.FirstAsync(c => c.Name == "Croatia");
            var serbia = await context.Countries.FirstAsync(c => c.Name == "Serbia");

            var cities = new List<City>
            {
                new City { Name = "Mostar",   CountryId = bosnia.Id },
                new City { Name = "Sarajevo", CountryId = bosnia.Id },
                new City { Name = "Zagreb",   CountryId = croatia.Id },
                new City { Name = "Beograd",  CountryId = serbia.Id }
            };

            context.Cities.AddRange(cities);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {cities.Count} cities added.");
        }

        private static async Task SeedBeltsAsync(DatabaseContext context)
        {
            if (await context.Belts.AnyAsync())
            {
                Console.WriteLine("⏭️  Belts already exist. Skipping seed.");
                return;
            }

            var belts = new List<Belt>
            {
                new Belt { Name = "White",       RankOrder = 1, Description = "Beginner" },
                new Belt { Name = "Yellow",      RankOrder = 2, Description = "8th kyu" },
                new Belt { Name = "Orange",      RankOrder = 3, Description = "7th kyu" },
                new Belt { Name = "Green",       RankOrder = 4, Description = "6th kyu" },
                new Belt { Name = "Blue",        RankOrder = 5, Description = "5th kyu" },
                new Belt { Name = "Brown",       RankOrder = 6, Description = "3-1 kyu" },
                new Belt { Name = "Black",       RankOrder = 7, Description = "Dan ranks" }
            };

            context.Belts.AddRange(belts);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {belts.Count} belts added.");
        }

        private static async Task SeedDisciplinesAsync(DatabaseContext context)
        {
            if (await context.Disciplines.AnyAsync())
            {
                Console.WriteLine("⏭️  Disciplines already exist. Skipping seed.");
                return;
            }

            var disciplines = new List<Discipline>
            {
                new Discipline { Name = "Kata",   Description = "Individual and team kata" },
                new Discipline { Name = "Kumite", Description = "Fighting discipline" },
                new Discipline { Name = "Kihon",  Description = "Basic techniques" }
            };

            context.Disciplines.AddRange(disciplines);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {disciplines.Count} disciplines added.");
        }

        #endregion

        #region Clubs & Locations

        private static async Task SeedClubsAsync(DatabaseContext context)
        {
            if (await context.Clubs.AnyAsync())
            {
                Console.WriteLine("⏭️  Clubs already exist. Skipping seed.");
                return;
            }

            var bosnia = await context.Countries.FirstAsync(c => c.Name == "Bosnia and Herzegovina");
            var croatia = await context.Countries.FirstAsync(c => c.Name == "Croatia");
            var serbia = await context.Countries.FirstAsync(c => c.Name == "Serbia");

            var mostar = await context.Cities.FirstAsync(c => c.Name == "Mostar");
            var sarajevo = await context.Cities.FirstAsync(c => c.Name == "Sarajevo");
            var zagreb = await context.Cities.FirstAsync(c => c.Name == "Zagreb");
            var beograd = await context.Cities.FirstAsync(c => c.Name == "Beograd");

            var clubs = new List<Club>
            {
                new Club
                {
                    Name = "KK Neretva Mostar",
                    Address = "Ul. Kralja Tomislava bb",
                    CityId = mostar.Id,
                    CountryId = bosnia.Id,
                    Email = "info@kkneretva.ba",
                    PhoneNumber = "+387 36 555 555",
                    Status = true
                },
                new Club
                {
                    Name = "KK Sarajevo",
                    Address = "Zmaja od Bosne 12",
                    CityId = sarajevo.Id,
                    CountryId = bosnia.Id,
                    Email = "kontakt@kksarajevo.ba",
                    PhoneNumber = "+387 33 222 222",
                    Status = true
                },
                new Club
                {
                    Name = "KK Zagreb",
                    Address = "Savska 10",
                    CityId = zagreb.Id,
                    CountryId = croatia.Id,
                    Email = "info@kkzagreb.hr",
                    PhoneNumber = "+385 1 111 111",
                    Status = true
                },
                new Club
                {
                    Name = "KK Beograd",
                    Address = "Knez Mihajlova 5",
                    CityId = beograd.Id,
                    CountryId = serbia.Id,
                    Email = "info@kkbg.rs",
                    PhoneNumber = "+381 11 333 333",
                    Status = true
                }
            };

            context.Clubs.AddRange(clubs);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {clubs.Count} clubs added.");
        }

        private static async Task SeedLocationsAsync(DatabaseContext context)
        {
            if (await context.Locations.AnyAsync())
            {
                Console.WriteLine("⏭️  Locations already exist. Skipping seed.");
                return;
            }

            var bosnia = await context.Countries.FirstAsync(c => c.Name == "Bosnia and Herzegovina");
            var croatia = await context.Countries.FirstAsync(c => c.Name == "Croatia");

            var mostar = await context.Cities.FirstAsync(c => c.Name == "Mostar");
            var sarajevo = await context.Cities.FirstAsync(c => c.Name == "Sarajevo");
            var zagreb = await context.Cities.FirstAsync(c => c.Name == "Zagreb");

            var locations = new List<Location>
            {
                new Location
                {
                    Title = "Sportska dvorana \"Midhat Hujdur Hujka\"",
                    Address = "Trg Slobode bb, Mostar",
                    CityId = mostar.Id,
                    CountryId = bosnia.Id,
                    Latitude = 43.3438,
                    Longitude = 17.8078
                },
                new Location
                {
                    Title = "Skenderija Arena",
                    Address = "Terezija 1, Sarajevo",
                    CityId = sarajevo.Id,
                    CountryId = bosnia.Id,
                    Latitude = 43.8564,
                    Longitude = 18.4131
                },
                new Location
                {
                    Title = "Dom Sportova Zagreb",
                    Address = "Trg Krešimira Ćosića 11, Zagreb",
                    CityId = zagreb.Id,
                    CountryId = croatia.Id,
                    Latitude = 45.8000,
                    Longitude = 15.9580
                }
            };

            context.Locations.AddRange(locations);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {locations.Count} locations added.");
        }

        #endregion

        #region Users, Coaches, Contestants, Judges

        /// <summary>
        /// Ovo je PRIMJER seeda za KTMSUserEntity.
        /// Obavezno prilagodi polja (FirstName, LastName, itd.) svojoj klasi!
        /// </summary>
        private static async Task SeedUsersAsync(DatabaseContext context)
        {
            var hasher = new PasswordHasher<KTMSUserEntity>();

            if (await context.Users.AnyAsync())
            {
                Console.WriteLine("⏭️  Users already exist. Skipping seed.");
                return;
            }

            var adminRole = await context.Roles.FirstAsync(r => r.Title == "Admin");
            var organizerRole = await context.Roles.FirstAsync(r => r.Title == "Organizer");
            var coachRole = await context.Roles.FirstAsync(r => r.Title == "Coach");
            var judgeRole = await context.Roles.FirstAsync(r => r.Title == "Judge");
            var contestantRole = await context.Roles.FirstAsync(r => r.Title == "Contestant");

            var mostar = await context.Cities.FirstAsync(c => c.Name == "Mostar");
            var sarajevo = await context.Cities.FirstAsync(c => c.Name == "Sarajevo");
            var zagreb = await context.Cities.FirstAsync(c => c.Name == "Zagreb");

            var male = await context.Genders.FirstAsync(g => g.Name == "Male");
            var female = await context.Genders.FirstAsync(g => g.Name == "Female");

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var admin = new KTMSUserEntity
            {
                RoleId = adminRole.Id,
                CityId = mostar.Id,
                GenderId = male.Id,
                Name = "System",
                Surname = "Admin",
                PhoneNumber = "+38761000000",
                Email = "admin@ktms.local",
                DateOfBirth = new DateOnly(1990, 1, 1),
                Username = "admin",
                Password = "Admin123!",
                RegistrationDate = DateTime.UtcNow.AddDays(-30),
                Status = true,
                IsAdmin = true,
                IsCoach = false,
                IsContestant = false,
                IsEnabled = true
            };

            var organizer = new KTMSUserEntity
            {
                RoleId = organizerRole.Id,
                CityId = sarajevo.Id,
                GenderId = male.Id,
                Name = "Nermin",
                Surname = "Organizator",
                PhoneNumber = "+38761111111",
                Email = "organizer@ktms.local",
                DateOfBirth = new DateOnly(1988, 5, 15),
                Username = "organizer",
                Password = "Organizer123!",
                RegistrationDate = DateTime.UtcNow.AddDays(-20),
                Status = true,
                IsAdmin = false,
                IsCoach = false,
                IsContestant = false,
                IsEnabled = true
            };

            var dummyForSwagger = new KTMSUserEntity
            {
                RoleId = organizerRole.Id,
                CityId = sarajevo.Id,
                GenderId = male.Id,
                Name = "Nermin",
                Surname = "Organizator",
                PhoneNumber = "+38761111111",
                Email = "string",
                DateOfBirth = new DateOnly(1988, 5, 15),
                Username = "organizer",
                Password = hasher.HashPassword(null!, "string"),
                RegistrationDate = DateTime.UtcNow.AddDays(-20),
                Status = true,
                IsAdmin = false,
                IsCoach = false,
                IsContestant = false,
                IsEnabled = true
            };

            var dummyForTests = new KTMSUserEntity
            {
                RoleId = organizerRole.Id,
                CityId = sarajevo.Id,
                GenderId = male.Id,
                Name = "Nermin",
                Surname = "Organizator",
                PhoneNumber = "+38761111111",
                Email = "string",
                DateOfBirth = new DateOnly(1988, 5, 15),
                Username = "organizer",
                Password = hasher.HashPassword(null!, "test123"),
                RegistrationDate = DateTime.UtcNow.AddDays(-20),
                Status = true,
                IsAdmin = false,
                IsCoach = false,
                IsContestant = false,
                IsEnabled = true
            };

            var coach1 = new KTMSUserEntity
            {
                RoleId = coachRole.Id,
                CityId = mostar.Id,
                GenderId = male.Id,
                Name = "Amir",
                Surname = "Trener",
                PhoneNumber = "+38761222222",
                Email = "coach.mostar@ktms.local",
                DateOfBirth = new DateOnly(1985, 3, 10),
                Username = "coach_mostar",
                Password = "Coach123!",
                RegistrationDate = DateTime.UtcNow.AddDays(-15),
                Status = true,
                IsAdmin = false,
                IsCoach = true,
                IsContestant = false,
                IsEnabled = true
            };

            var coach2 = new KTMSUserEntity
            {
                RoleId = coachRole.Id,
                CityId = zagreb.Id,
                GenderId = female.Id,
                Name = "Ivana",
                Surname = "Trenerica",
                PhoneNumber = "+385911234567",
                Email = "coach.zagreb@ktms.local",
                DateOfBirth = new DateOnly(1992, 7, 22),
                Username = "coach_zagreb",
                Password = "Coach123!",
                RegistrationDate = DateTime.UtcNow.AddDays(-12),
                Status = true,
                IsAdmin = false,
                IsCoach = true,
                IsContestant = false,
                IsEnabled = true
            };

            var judgeUser = new KTMSUserEntity
            {
                RoleId = judgeRole.Id,
                CityId = sarajevo.Id,
                GenderId = male.Id,
                Name = "Marko",
                Surname = "Sudija",
                PhoneNumber = "+38761333333",
                Email = "judge@ktms.local",
                DateOfBirth = new DateOnly(1980, 9, 5),
                Username = "judge",
                Password = "Judge123!",
                RegistrationDate = DateTime.UtcNow.AddDays(-18),
                Status = true,
                IsAdmin = false,
                IsCoach = false,
                IsContestant = false,
                IsEnabled = true
            };

            var contestantUser1 = new KTMSUserEntity
            {
                RoleId = contestantRole.Id,
                CityId = mostar.Id,
                GenderId = female.Id,
                Name = "Lejla",
                Surname = "Karate",
                PhoneNumber = "+38761444444",
                Email = "lejla.karate@ktms.local",
                DateOfBirth = new DateOnly(2010, 4, 12),
                Username = "lejla.karate",
                Password = "Contest123!",
                RegistrationDate = DateTime.UtcNow.AddDays(-10),
                Status = true,
                IsAdmin = false,
                IsCoach = false,
                IsContestant = true,
                IsEnabled = true
            };

            var contestantUser2 = new KTMSUserEntity
            {
                RoleId = contestantRole.Id,
                CityId = zagreb.Id,
                GenderId = male.Id,
                Name = "Ivan",
                Surname = "Fighter",
                PhoneNumber = "+385912222222",
                Email = "ivan.fighter@ktms.local",
                DateOfBirth = new DateOnly(2009, 11, 3),
                Username = "ivan.fighter",
                Password = "Contest123!",
                RegistrationDate = DateTime.UtcNow.AddDays(-9),
                Status = true,
                IsAdmin = false,
                IsCoach = false,
                IsContestant = true,
                IsEnabled = true
            };

            var users = new[]
            {
               admin,
               organizer,
               coach1,
               coach2,
               judgeUser,
               contestantUser1,
               contestantUser2,
               dummyForSwagger,
               dummyForTests
            };

            foreach (var u in users)
            {
                u.Password = hasher.HashPassword(u, u.Password);
            }

            context.Users.AddRange(users);

            await context.SaveChangesAsync();
            Console.WriteLine("✅ Dynamic seed: demo users added.");
        }

        private static async Task SeedCoachesAsync(DatabaseContext context)
        {
            if (await context.Coaches.AnyAsync())
            {
                Console.WriteLine("⏭️  Coaches already exist. Skipping seed.");
                return;
            }

            var beltBlack = await context.Belts.FirstAsync(b => b.Name == "Black");
            var beltBrown = await context.Belts.FirstAsync(b => b.Name == "Brown");

            var clubMostar = await context.Clubs.FirstAsync(c => c.Name.Contains("Neretva"));
            var clubZagreb = await context.Clubs.FirstAsync(c => c.Name.Contains("Zagreb"));

            var coachUser1 = await context.Users.FirstAsync(u => u.Email == "coach.mostar@ktms.local");
            var coachUser2 = await context.Users.FirstAsync(u => u.Email == "coach.zagreb@ktms.local");

            var coaches = new List<Coach>
            {
                new Coach
                {
                    UserId = coachUser1.Id,
                    ClubId = clubMostar.Id,
                    BeltId = beltBlack.Id,
                    LicenseNumber = "COACH-001",
                    CertificationLevel = "National A",
                    Verified = true
                },
                new Coach
                {
                    UserId = coachUser2.Id,
                    ClubId = clubZagreb.Id,
                    BeltId = beltBrown.Id,
                    LicenseNumber = "COACH-002",
                    CertificationLevel = "National B",
                    Verified = true
                }
            };

            context.Coaches.AddRange(coaches);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {coaches.Count} coaches added.");
        }

        private static async Task SeedContestantsAsync(DatabaseContext context)
        {
            if (await context.Contestants.AnyAsync())
            {
                Console.WriteLine("⏭️  Contestants already exist. Skipping seed.");
                return;
            }

            var beltGreen = await context.Belts.FirstAsync(b => b.Name == "Green");
            var beltBlue = await context.Belts.FirstAsync(b => b.Name == "Blue");
            var clubMostar = await context.Clubs.FirstAsync(c => c.Name.Contains("Neretva"));
            var clubZagreb = await context.Clubs.FirstAsync(c => c.Name.Contains("Zagreb"));

            var u1 = await context.Users.FirstAsync(u => u.Email == "lejla.karate@ktms.local");
            var u2 = await context.Users.FirstAsync(u => u.Email == "ivan.fighter@ktms.local");

            var contestants = new List<Contestant>
            {
                new Contestant
                {
                    UserId = u1.Id,
                    ClubId = clubMostar.Id,
                    BeltId = beltBlue.Id
                },
                new Contestant
                {
                    UserId = u2.Id,
                    ClubId = clubZagreb.Id,
                    BeltId = beltGreen.Id
                }
            };

            context.Contestants.AddRange(contestants);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {contestants.Count} contestants added.");
        }

        private static async Task SeedJudgesAsync(DatabaseContext context)
        {
            if (await context.Judges.AnyAsync())
            {
                Console.WriteLine("⏭️  Judges already exist. Skipping seed.");
                return;
            }

            var judgeUser = await context.Users.FirstAsync(u => u.Email == "judge@ktms.local");

            var judges = new List<Judge>
            {
                new Judge
                {
                    UserId = judgeUser.Id,
                    License = "JUDGE-INT-A",
                    Rank = "WKF International A"
                }
            };

            context.Judges.AddRange(judges);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {judges.Count} judges added.");
        }

        #endregion

        #region Tournaments, Tatamis, Categories

        private static async Task SeedTournamentsAsync(DatabaseContext context)
        {
            if (await context.Tournaments.AnyAsync())
            {
                Console.WriteLine("⏭️  Tournaments already exist. Skipping seed.");
                return;
            }

            var mostarArena = await context.Locations.FirstAsync(l => l.Title.Contains("Hujdur"));
            var sarajevoArena = await context.Locations.FirstAsync(l => l.Title.Contains("Skenderija"));

            var tournaments = new List<Tournament>
            {
                new Tournament
                {
                    Title = "Mostar Open 2026",
                    Description = "Međunarodni karate turnir u Mostaru.",
                    LocationId = mostarArena.Id,
                    Date = new DateOnly(2026, 3, 15),
                    StartTime = new TimeOnly(10, 0),
                    EndTime = new TimeOnly(18, 0),
                    RegistrationFee = "20 EUR",
                    Status = "Open"
                },
                new Tournament
                {
                    Title = "Sarajevo Cup 2026",
                    Description = "Državno prvenstvo u karateu.",
                    LocationId = sarajevoArena.Id,
                    Date = new DateOnly(2026, 4, 10),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(19, 0),
                    RegistrationFee = "25 EUR",
                    Status = "Planned"
                }
            };

            context.Tournaments.AddRange(tournaments);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {tournaments.Count} tournaments added.");
        }

        private static async Task SeedTournamentJudgesAsync(DatabaseContext context)
        {
            if (await context.TournamentJudges.AnyAsync())
            {
                Console.WriteLine("⏭️  TournamentJudges already exist. Skipping seed.");
                return;
            }

            var judge = await context.Judges.FirstAsync();
            var tournaments = await context.Tournaments.ToListAsync();

            var tjs = tournaments.Select(t => new TournamentJudge
            {
                JudgeId = judge.Id,
                TournamentId = t.Id,
                Role = "Chief Judge"
            }).ToList();

            context.TournamentJudges.AddRange(tjs);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {tjs.Count} tournament judges added.");
        }

        private static async Task SeedTatamisAsync(DatabaseContext context)
        {
            if (await context.Tatamis.AnyAsync())
            {
                Console.WriteLine("⏭️  Tatamis already exist. Skipping seed.");
                return;
            }

            var judge = await context.Judges.FirstAsync();
            var tournaments = await context.Tournaments.ToListAsync();

            var tatamis = new List<Tatami>();

            foreach (var t in tournaments)
            {
                tatamis.Add(new Tatami
                {
                    TournamentId = t.Id,
                    JudgeId = judge.Id,
                    Number = 1
                });
                tatamis.Add(new Tatami
                {
                    TournamentId = t.Id,
                    JudgeId = judge.Id,
                    Number = 2
                });
            }

            context.Tatamis.AddRange(tatamis);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {tatamis.Count} tatamis added.");
        }

        private static async Task SeedCategoriesAsync(DatabaseContext context)
        {
            if (await context.Categories.AnyAsync())
            {
                Console.WriteLine("⏭️  Categories already exist. Skipping seed.");
                return;
            }

            var kata = await context.Disciplines.FirstAsync(d => d.Name == "Kata");
            var kumite = await context.Disciplines.FirstAsync(d => d.Name == "Kumite");
            var male = await context.Genders.FirstAsync(g => g.Name == "Male");
            var female = await context.Genders.FirstAsync(g => g.Name == "Female");

            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Kata U14 M",
                    AgeGroup = "U14",
                    WeightClass = "Open",
                    DisciplineId = kata.Id,
                    GenderId = male.Id
                },
                new Category
                {
                    Name = "Kata U16 F",
                    AgeGroup = "U16",
                    WeightClass = "Open",
                    DisciplineId = kata.Id,
                    GenderId = female.Id
                },
                new Category
                {
                    Name = "Kumite -57kg U18 F",
                    AgeGroup = "U18",
                    WeightClass = "-57kg",
                    DisciplineId = kumite.Id,
                    GenderId = female.Id
                },
                new Category
                {
                    Name = "Kumite -70kg U18 M",
                    AgeGroup = "U18",
                    WeightClass = "-70kg",
                    DisciplineId = kumite.Id,
                    GenderId = male.Id
                }
            };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {categories.Count} categories added.");
        }

        #endregion

        #region Enrollments, TournamentContestants, Payments, Schedules, Brackets, Results, Notifications

        private static async Task SeedTournamentContestantsAsync(DatabaseContext context)
        {
            if (await context.TournamentContestants.AnyAsync())
            {
                Console.WriteLine("⏭️  TournamentContestants already exist. Skipping seed.");
                return;
            }

            var tournaments = await context.Tournaments.ToListAsync();
            var contestants = await context.Contestants.ToListAsync();

            if (!tournaments.Any() || !contestants.Any())
                return;

            var links = new List<TournamentContestant>
            {
                new TournamentContestant
                {
                    TournamentId = tournaments[0].Id,
                    ContestantId = contestants[0].Id
                },
                new TournamentContestant
                {
                    TournamentId = tournaments[0].Id,
                    ContestantId = contestants[1].Id
                },
                new TournamentContestant
                {
                    TournamentId = tournaments[1].Id,
                    ContestantId = contestants[0].Id
                }
            };

            context.TournamentContestants.AddRange(links);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {links.Count} tournament contestants linked.");
        }

        private static async Task SeedEnrollmentsAsync(DatabaseContext context)
        {
            if (await context.Enrollments.AnyAsync())
            {
                Console.WriteLine("⏭️  Enrollments already exist. Skipping seed.");
                return;
            }

            var tournaments = await context.Tournaments.ToListAsync();
            var contestants = await context.Contestants.ToListAsync();
            var kata = await context.Disciplines.FirstAsync(d => d.Name == "Kata");
            var kumite = await context.Disciplines.FirstAsync(d => d.Name == "Kumite");
            var catKataU14M = await context.Categories.FirstAsync(c => c.Name == "Kata U14 M");
            var catKataU16F = await context.Categories.FirstAsync(c => c.Name == "Kata U16 F");
            var catKumiteF = await context.Categories.FirstAsync(c => c.Name.Contains("U18 F"));
            var catKumiteM = await context.Categories.FirstAsync(c => c.Name.Contains("U18 M"));

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var enrollments = new List<Enrollment>
            {
                new Enrollment
                {
                    TournamentId = tournaments[0].Id,
                    ContestantId = contestants[0].Id,
                    DisciplineId = kata.Id,
                    CategoryId = catKataU16F.Id,
                    ApplicationDate = today.AddDays(-10),
                    Status = "Approved",
                    Paid = true
                },
                new Enrollment
                {
                    TournamentId = tournaments[0].Id,
                    ContestantId = contestants[1].Id,
                    DisciplineId = kumite.Id,
                    CategoryId = catKumiteM.Id,
                    ApplicationDate = today.AddDays(-8),
                    Status = "Approved",
                    Paid = false
                },
                new Enrollment
                {
                    TournamentId = tournaments[1].Id,
                    ContestantId = contestants[0].Id,
                    DisciplineId = kumite.Id,
                    CategoryId = catKumiteF.Id,
                    ApplicationDate = today.AddDays(-5),
                    Status = "Pending",
                    Paid = false
                }
            };

            context.Enrollments.AddRange(enrollments);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {enrollments.Count} enrollments added.");
        }

        private static async Task SeedPaymentsAsync(DatabaseContext context)
        {
            if (await context.Payments.AnyAsync())
            {
                Console.WriteLine("⏭️  Payments already exist. Skipping seed.");
                return;
            }

            var enrollments = await context.Enrollments.ToListAsync();

            if (!enrollments.Any())
                return;

            var payments = new List<Payment>
            {
                new Payment
                {
                    EnrollmentId = enrollments[0].Id,
                    Amount = 20,
                    PaymentDate = DateTime.UtcNow.AddDays(-9),
                    PaymentMethod = "Bank Transfer",
                    Status = "Completed"
                },
                new Payment
                {
                    EnrollmentId = enrollments[1].Id,
                    Amount = 20,
                    PaymentDate = DateTime.UtcNow.AddDays(-7),
                    PaymentMethod = "Cash",
                    Status = "Pending"
                }
            };

            context.Payments.AddRange(payments);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {payments.Count} payments added.");
        }

        private static async Task SeedSchedulesAsync(DatabaseContext context)
        {
            if (await context.Schedules.AnyAsync())
            {
                Console.WriteLine("⏭️  Schedules already exist. Skipping seed.");
                return;
            }

            var tournaments = await context.Tournaments.ToListAsync();
            var tatamis = await context.Tatamis.ToListAsync();
            var catKataU16F = await context.Categories.FirstAsync(c => c.Name == "Kata U16 F");
            var catKumiteM = await context.Categories.FirstAsync(c => c.Name.Contains("U18 M"));

            var schedules = new List<Schedule>
            {
                new Schedule
                {
                    TournamentId = tournaments[0].Id,
                    CategoryId = catKataU16F.Id,
                    TatamiId = tatamis.First(t => t.TournamentId == tournaments[0].Id && t.Number == 1).Id,
                    Date = tournaments[0].Date,
                    StartTime = new TimeOnly(10, 0),
                    EndTime = new TimeOnly(11, 30)
                },
                new Schedule
                {
                    TournamentId = tournaments[0].Id,
                    CategoryId = catKumiteM.Id,
                    TatamiId = tatamis.First(t => t.TournamentId == tournaments[0].Id && t.Number == 2).Id,
                    Date = tournaments[0].Date,
                    StartTime = new TimeOnly(12, 0),
                    EndTime = new TimeOnly(14, 0)
                }
            };

            context.Schedules.AddRange(schedules);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {schedules.Count} schedules added.");
        }

        private static async Task SeedBracketsAsync(DatabaseContext context)
        {
            if (await context.Brackets.AnyAsync())
            {
                Console.WriteLine("⏭️  Brackets already exist. Skipping seed.");
                return;
            }

            var tournament = await context.Tournaments.FirstAsync(t => t.Title == "Mostar Open 2026");
            var tatami1 = await context.Tatamis.FirstAsync(t => t.TournamentId == tournament.Id && t.Number == 1);
            var contestants = await context.Contestants.ToListAsync();

            if (contestants.Count < 2)
                return;

            var brackets = new List<Bracket>
            {
                new Bracket
                {
                    TournamentId = tournament.Id,
                    TatamiId = tatami1.Id,
                    ContestantOneId = contestants[0].Id,
                    ContestantTwoId = contestants[1].Id,
                    Round = "Final",
                    Result = "3:2"
                }
            };

            context.Brackets.AddRange(brackets);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {brackets.Count} brackets added.");
        }

        private static async Task SeedResultsAsync(DatabaseContext context)
        {
            if (await context.Results.AnyAsync())
            {
                Console.WriteLine("⏭️  Results already exist. Skipping seed.");
                return;
            }

            var tournament = await context.Tournaments.FirstAsync(t => t.Title == "Mostar Open 2026");
            var contestants = await context.Contestants.ToListAsync();
            var kumite = await context.Disciplines.FirstAsync(d => d.Name == "Kumite");
            var catKumiteM = await context.Categories.FirstAsync(c => c.Name.Contains("U18 M"));

            var results = new List<Result>
            {
                new Result
                {
                    TournamentId = tournament.Id,
                    ContestantId = contestants[1].Id,
                    DisciplineId = kumite.Id,
                    CategoryId = catKumiteM.Id,
                    Position = "1st",
                    Points = 10,
                    Award = "Gold"
                },
                new Result
                {
                    TournamentId = tournament.Id,
                    ContestantId = contestants[0].Id,
                    DisciplineId = kumite.Id,
                    CategoryId = catKumiteM.Id,
                    Position = "2nd",
                    Points = 7,
                    Award = "Silver"
                }
            };

            context.Results.AddRange(results);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {results.Count} results added.");
        }

        private static async Task SeedNotificationsAsync(DatabaseContext context)
        {
            if (await context.Notifications.AnyAsync())
            {
                Console.WriteLine("⏭️  Notifications already exist. Skipping seed.");
                return;
            }

            var tournament = await context.Tournaments.FirstAsync(t => t.Title == "Mostar Open 2026");

            var notifications = new List<Notification>
            {
                new Notification
                {
                    TournamentId = tournament.Id,
                    Title = "Dobrodošlica učesnicima",
                    Content = "Dobrodošli na Mostar Open 2026! Molimo trenere da potvrde prisustvo na tehničkom sastanku u 09:00.",
                    PublishDate = DateTime.UtcNow.AddDays(-3),
                    Author = "Organizing Committee"
                },
                new Notification
                {
                    TournamentId = tournament.Id,
                    Title = "Promjena rasporeda",
                    Content = "Kategorija Kumite -70kg U18 M pomjerena je na 12:30 na tatami 2.",
                    PublishDate = DateTime.UtcNow.AddDays(-1),
                    Author = "Chief Judge"
                }
            };

            context.Notifications.AddRange(notifications);
            await context.SaveChangesAsync();

            Console.WriteLine($"✅ Dynamic seed: {notifications.Count} notifications added.");
        }

        #endregion
    }
}