using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Concrete
{
    public partial class Order
    {
        public Order()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public decimal? Total { get; set; }
        public DateTime? OrderDate { get; set; }
        public bool? OrderState { get; set; }
        public string Username { get; set; }
        public string AdresTanimi { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Semt { get; set; }
        public string Telefon { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
