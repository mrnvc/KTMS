using KTMS.Application.Modules.Clubs.Dtos;
using MediatR;

namespace KTMS.Application.Modules.Clubs.Queries.GetClubsById
{
    public class GetClubsByIdQuery : IRequest<ClubDto>
    {
        public int Id { get; set; }
        public GetClubsByIdQuery(int id)
        {
            Id = id;
        }
    }
}
