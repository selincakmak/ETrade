using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Concrete
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public decimal? Total { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
