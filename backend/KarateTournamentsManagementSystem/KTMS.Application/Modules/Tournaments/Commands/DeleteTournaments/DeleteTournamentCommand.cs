using KTMS.Application.Modules.Tournaments.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Tournaments.Commands.DeleteTournaments
{
    public class DeleteTournamentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
