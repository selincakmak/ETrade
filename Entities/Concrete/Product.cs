using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Concrete
{
    public partial class Product
    {
        public Product()
        {
            Images = new HashSet<Image>();
            ProductCategories = new HashSet<ProductCategory>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsHome { get; set; }
        public bool? IsFeatured { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public DateTime? DateAdded { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
