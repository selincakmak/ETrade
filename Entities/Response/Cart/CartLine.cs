using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response.Cart
{

   public class Cart
    {
        private List<CartLine> products = new List<CartLine>();
        public List<CartLine> Products => products;
        
        public void AddProduct(Entities.Concrete.Product product,int quantity)
        {
            var prd = products.Where(i => i.Product.ProductId == product.ProductId).FirstOrDefault();
            if (prd == null)
            {
                products.Add(new CartLine()
                {
                    Product = product,
                    Quantity=quantity
                });
            }
            else
            {
                prd.Quantity += quantity;
            }

        }

        public void RemoveProduct(Entities.Concrete.Product product)
        {
            products.RemoveAll(i => i.Product.ProductId == product.ProductId);
        }

        public double TotalPrice()
        { var value= products.Sum(i => i.Product.Price * i.Quantity);
            return (double)value;
        }
    
        public void ClearAll()
        {
            products.Clear();
        }
            
            
            
            }
    public class CartLine

    {
        public int CartLineId { get; set; }
        public Entities.Concrete.Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
