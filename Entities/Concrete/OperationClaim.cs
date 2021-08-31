using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Concrete
{
    public partial class OperationClaim
    {
        public OperationClaim()
        {
            UserOperationClaims = new HashSet<UserOperationClaim>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
