using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Concrete
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
