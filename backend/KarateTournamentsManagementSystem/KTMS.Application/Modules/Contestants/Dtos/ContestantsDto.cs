using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Contestants.Dtos
{
    public class ContestantsDto
    {
        public required string User {  get; set; }
        public required string Belt { get; set; }
        public required string Club { get; set; }
    }
}
