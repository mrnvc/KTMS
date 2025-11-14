using KTMS.Application.Modules.Clubs.Dtos;
using MediatR;

namespace KTMS.Application.Modules.Clubs.Queries
{
    public class GetClubsQuery : IRequest<List<ClubDto>>
    {
    }
}
