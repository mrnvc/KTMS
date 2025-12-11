using KTMS.Application.Modules.Tournaments.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Tournaments.Queries.GetTournamentsById
{
    public class GetTournamentsByIdQuery : IRequest<TournamentsDto>
    {
        public int Id { get; set; }
        public GetTournamentsByIdQuery(int id)
        {
            Id = id;
        }
    }

}
