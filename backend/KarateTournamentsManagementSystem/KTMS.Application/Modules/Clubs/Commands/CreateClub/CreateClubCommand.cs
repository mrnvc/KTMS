using KTMS.Application.Modules.Clubs.Dtos;
using MediatR;

namespace KTMS.Application.Modules.Clubs.Commands.CreateClub
{
    public class CreateClubCommand : IRequest<int>
    {
        public CreateClubDto CreateClubDto { get; set; }
    }
}
