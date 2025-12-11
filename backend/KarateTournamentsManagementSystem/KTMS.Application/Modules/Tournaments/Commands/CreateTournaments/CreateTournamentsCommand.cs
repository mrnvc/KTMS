using KTMS.Application.Modules.Tournaments.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Tournaments.Commands.CreateTournaments
{
    public class CreateTournamentsCommand : IRequest<int>
    {
        public CreateTournamentsDto CreateTournamentsDto { get; set; }

    }
}
