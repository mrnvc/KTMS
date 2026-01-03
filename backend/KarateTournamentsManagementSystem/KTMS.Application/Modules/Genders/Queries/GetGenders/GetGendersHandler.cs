using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Genders.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks.Sources;

namespace KTMS.Application.Modules.Genders.Queries.GetGenders
{
    public class GetGendersHandler : IRequestHandler<GetGendersQuery, List<GenderDto>>
    {
        private readonly IAppDbContext _dbContext;

        public GetGendersHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GenderDto>> Handle(GetGendersQuery request, CancellationToken cancellationToken)
        {
            var genders = await _dbContext.Genders
                                          .Select(g => new GenderDto
                                          {
                                              Id = g.Id,
                                              Name = g.Name
                                          })
                                          .ToListAsync(cancellationToken);

            return genders;
        }
    }
}
