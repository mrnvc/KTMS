namespace KTMS.Application.Modules.Contestants.Dtos
{
    public class ContestantsDto
    {
        public int Id { get; set; }
        public required string User {  get; set; }
        public required string Belt { get; set; }
        public required string Club { get; set; }
    }
}
