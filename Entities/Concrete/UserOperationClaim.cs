using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Concrete
{
    public partial class UserOperationClaim
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? OperationClaimId { get; set; }

        public virtual OperationClaim OperationClaim { get; set; }
        public virtual User User { get; set; }
    }
}
