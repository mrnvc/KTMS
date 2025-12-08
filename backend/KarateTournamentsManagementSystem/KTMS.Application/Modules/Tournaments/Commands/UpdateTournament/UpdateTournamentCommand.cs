using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTMS.Application.Modules.Tournaments.Dtos;
using MediatR;

namespace KTMS.Application.Modules.Tournaments.Commands.UpdateTournament
{
   public class UpdateTournamentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public UpdateTournamentDto UpdateTournamentDto { get; set; }
    }
}
