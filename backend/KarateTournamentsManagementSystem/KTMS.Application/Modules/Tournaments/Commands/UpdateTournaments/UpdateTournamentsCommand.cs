using KTMS.Application.Modules.Tournaments.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Tournaments.Commands.UpdateTournaments
{
    public class UpdateTournamentsCommand : IRequest<int>
    {
        public int Id { get; set; }
        public UpdateTournamentsDto UpdateTournamentsDto { get; set; }
    }

}
