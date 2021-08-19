using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Concrete
{
    public partial class Attribute
    {
        public int ProductAttributeId { get; set; }
        public string Attribute1 { get; set; }
        public string Value { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
