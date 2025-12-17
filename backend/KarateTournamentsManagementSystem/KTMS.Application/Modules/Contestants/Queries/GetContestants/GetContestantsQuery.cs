using KTMS.Application.Modules.Contestants.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Contestants.Queries.GetContestants
{
    public class GetContestantsQuery: IRequest<List<ContestantsDto>>
    {
    }
}
