using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Response.Category
{
   public class CategoryModel //kategorideki ürün sayısını tutmak için
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int Count { get; set; }
    }
}
