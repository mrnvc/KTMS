using KTMS.Application.Modules.Judges.Dtos;
using MediatR;

namespace KTMS.Application.Modules.Judges.Commands.UpdateJudge
{
    public class UpdateJudgeCommand : IRequest<int>
    {
        public int Id { get; set; }
        public UpdateJudgeDto UpdateJudgeDto { get; set; }
    }
}
