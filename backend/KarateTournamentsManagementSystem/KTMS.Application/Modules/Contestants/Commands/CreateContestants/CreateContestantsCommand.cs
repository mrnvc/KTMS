using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTMS.Application.Modules.Contestants.Dtos;

namespace KTMS.Application.Modules.Contestants.Commands.CreateContestants
{
    public class CreateContestantsCommand : IRequest<int>
    { 
        public CreateContestantsDto CreateContestantsDto { get; set; }
    }
}
