using KTMS.Application.Modules.Contestants.Dtos;

namespace KTMS.Application.Modules.Contestants.Queries.GetContestantsById
{
    public class GetContestantsByIdQuery: IRequest<ContestantDetailsDto>
    {
        public int Id { get; set; }
        public GetContestantsByIdQuery(int id)
        {
            Id = id;
        }
    }
}
