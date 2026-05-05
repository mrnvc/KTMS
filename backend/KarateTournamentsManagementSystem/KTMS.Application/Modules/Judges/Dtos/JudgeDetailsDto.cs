namespace KTMS.Application.Modules.Judges.Dtos
{
    public class JudgeDetailsDto
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

        public required string License { get; set; }
        public string? Rank { get; set; }
    }
}