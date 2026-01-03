namespace KTMS.Application.Modules.Roles.Dtos
{
    public class RoleDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
    }
}
