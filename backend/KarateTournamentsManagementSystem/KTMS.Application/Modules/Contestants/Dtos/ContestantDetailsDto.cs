namespace KTMS.Application.Modules.Contestants.Dtos
{
    public class ContestantDetailsDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public required string Username { get; set; }

        public int CityId { get; set; }
        public int GenderId { get; set; }
        public int BeltId { get; set; }
        public int ClubId { get; set; }
    }
}