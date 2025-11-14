using KTMS.Application.Common.Paging;
using KTMS.Application.Modules.Clubs.Dtos;

namespace KTMS.Application.Modules.Clubs.Queries.GetClubsFiltered
{
    public class GetClubsFilteredQuery : BasePagedQuery<ClubDto>
    {
        public string? Name { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? Status { get; set; }

        //public string City { get; set; }
        //public string Country { get; set; }
    }
}
