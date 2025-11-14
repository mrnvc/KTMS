namespace KTMS.Application.Modules.Judges.Dtos
{
    public class CreateJudgeDto
    {
        public int UserId { get; set; }
        public string License { get; set; }
        public string? Rank { get; set; }
    }
}
