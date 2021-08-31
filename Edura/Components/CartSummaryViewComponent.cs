using Edura.Infrastructure;
using Entities.Response.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edura.Components
{
    public class CartSummaryViewComponent:ViewComponent
    {
        public string Invoke()
        {
            return HttpContext.Session.GetObjectFromJson<Cart>("Cart")?.Products.Count().ToString() ?? "0";
        }
    }
}
