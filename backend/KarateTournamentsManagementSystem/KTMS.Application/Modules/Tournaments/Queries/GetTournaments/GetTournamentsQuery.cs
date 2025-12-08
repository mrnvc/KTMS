using KTMS.Application.Modules.Tournaments.Dtos;
using KTMS.Application.Modules.Users.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Tournaments.Queries.GetTournaments
{
    public class GetTournamentsQuery: IRequest<List<TournamentDto>>
    {
    }
}
