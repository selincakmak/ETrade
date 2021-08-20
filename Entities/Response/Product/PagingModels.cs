using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response.Product
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);

        }

    }

    public class ProductListModel
    {
        public IEnumerable<Entities.Concrete.Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
}
