using KTMS.Application.Modules.Judges.Dtos;
using MediatR;

namespace KTMS.Application.Modules.Judges.Commands.CreateJudge
{
    public class CreateJudgeCommand : IRequest<int>
    {
        public CreateJudgeDto CreateJudgeDto { get; set; }
    }
}
