using KTMS.Application.Modules.Contestants.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Contestants.Commands.UpdateContestants
{
    public class UpdateContestantsCommand: IRequest<int>
    { 
        public int Id { get; set; }
        public UpdateContestantsDto UpdateContestantsDto { get; set; }

    }
}
