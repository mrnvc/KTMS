using MediatR;

namespace KTMS.Application.Modules.Judges.Commands.DeleteJudge
{
    public class DeleteJudgeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
