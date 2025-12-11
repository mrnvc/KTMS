using KTMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Tournaments.Dtos
{
    public class CreateTournamentsDto
    {
        public int LocationId { get; set; }
        public required string Title { get; set; }
        public DateOnly Date { get; set; }
        public required TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public string? Description { get; set; }
        public required string RegistrationFee { get; set; }
        public required string Status { get; set; }

    }
}

