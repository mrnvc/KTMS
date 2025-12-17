using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.Contestants.Dtos
{
   public class UpdateContestantsDto
    {   
        public required int UserId { get; set; }
        public required int BeltId { get; set; }
        public required int ClubId { get; set; }
    }
}
