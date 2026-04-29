using KTMS.Application.Modules.Contestants.Dtos;

namespace KTMS.Application.Modules.Contestants.Commands.CreateContestants
{
    public class CreateContestantsCommand : IRequest<int>
    { 
        public CreateContestantsDto CreateContestantsDto { get; set; }
    }
}
