using KTMS.Application.Modules.Tournaments.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Tournaments.Queries.GetTournamentsFiltered
{
   public class GetTournamentsFilteredQuery : IRequest<List<TournamentsDto>>
    {
        public string? Title {get; set;}
        public string? City { get; set;}
        public string? Country { get; set;}
        public string? Adress { get; set;}
        public DateOnly? Date { get; set;}
        public int? Year { get; set;}
        public int? Month { get; set;}
        public int? Day { get; set;}



    }
}
