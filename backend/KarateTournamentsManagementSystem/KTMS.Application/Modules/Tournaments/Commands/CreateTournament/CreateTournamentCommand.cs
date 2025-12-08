using KTMS.Application.Modules.Tournaments.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Tournaments.Commands.CreateTournament
{
   public class CreateTournamentCommand: IRequest<int>
    {
       public CreateTournamentDto CreateTournamentDto { get; set; }

    }
}
