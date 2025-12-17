using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Contestants.Commands.DeleteContestants
{
    public class DeleteContestantsCommand: IRequest<bool>
    {
        public int Id { get; set; }
    }
}
