﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Concrete
{
    public partial class OrderLine
    {
        public int OrderLineId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
