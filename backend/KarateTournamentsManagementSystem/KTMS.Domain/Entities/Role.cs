using KTMS.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }

        //Collections
        public ICollection<KTMSUserEntity> Users { get; set; } = new List<KTMSUserEntity>();
    }
}
