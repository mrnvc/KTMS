namespace KTMS.Application.Modules.Judges.Dtos
{
    public class JudgeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string License { get; set; }
        public string Rank { get; set; }
    }
}
