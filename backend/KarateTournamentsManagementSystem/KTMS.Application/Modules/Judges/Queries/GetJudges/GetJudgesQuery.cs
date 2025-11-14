using KTMS.Application.Modules.Judges.Dtos;
using MediatR;

namespace KTMS.Application.Modules.Judges.Queries.GetJudges
{
    public class GetJudgesQuery : IRequest<List<JudgeDto>>
    {

    }
}
