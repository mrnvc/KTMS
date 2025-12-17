using KTMS.Application.Modules.Contestants.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Contestants.Queries.GetContestantsById
{
    public class GetContestantsByIdQuery: IRequest<ContestantsDto>
    {
        public int Id { get; set; }
        public GetContestantsByIdQuery(int id)
        {
            Id = id;
        }
    }
}
