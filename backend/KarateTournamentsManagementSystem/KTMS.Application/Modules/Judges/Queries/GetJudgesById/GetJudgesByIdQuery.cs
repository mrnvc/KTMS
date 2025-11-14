using KTMS.Application.Modules.Judges.Dtos;
using MediatR;

namespace KTMS.Application.Modules.Judges.Queries.GetJudgesById
{
    public class GetJudgesByIdQuery : IRequest<JudgeDto>
    {
        public int Id { get; set; }
        public GetJudgesByIdQuery(int id)
        {
            Id = id;
        }
    }
}
