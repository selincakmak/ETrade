using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Entities.Response.Product
{
   public class ProductDetailsModel
    {
        public Concrete.Product Product { get; set; }
        public List<Image> ProductImages { get; set; }

        public List<Concrete.Attribute> ProductAttributes { get; set; }

        public List<string>  Categories { get; set; }
    }
}
