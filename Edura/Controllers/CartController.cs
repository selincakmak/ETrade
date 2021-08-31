using Business.Utilities.Jwt;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Response.Cart;
using Entities.Response.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Edura.Infrastructure;
using Edura.Models;
using Microsoft.AspNetCore.Authorization;
using Edura.Helper;

namespace Edura.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View(GetCart());
        }

        public IActionResult AddToCart(int ProductId, int quantity = 1)
        {
            UserForLoginDto loginDtos = new UserForLoginDto();
            var token = apiResponse(loginDtos).Result;
            if (token != "")
            {
                var product = getDetail(token, ProductId).Result;

                if (product != null)
                {
                    var cart = GetCart();
                    cart.AddProduct(product.Product, quantity);
                    SaveCart(cart);
                }

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int ProductId)
        {
            UserForLoginDto loginDtos = new UserForLoginDto();
            var token = apiResponse(loginDtos).Result;
            if (token != "")
            {
                var product = getDetail(token, ProductId).Result;

                if (product != null)
                {
                    var cart = GetCart();
                    cart.RemoveProduct(product.Product);
                    SaveCart(cart);
                }

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        [AdminAuthorizationHelper]
        public IActionResult Checkout()
        {
            return View();
        }


        [HttpPost]
     
        public IActionResult Checkout(OrderDetails model)
        {
            var cart = GetCart();
            if (cart.Products.Count == 0)
            {
                ModelState.AddModelError("UrunYokModel", "Sepetinizde ürün bulunamadı");
            }
            if (ModelState.IsValid)
            {
                var result = SaveOrder(cart, model);
                
                if (result == true)
                {
                    cart.ClearAll();
                    SaveCart(cart);
                    return View("Completed");
                }
                else
                    return View();

            }
            return View();
        }

        private bool SaveOrder(Cart cart, OrderDetails details)
        {
            UserForLoginDto loginDtos = new UserForLoginDto();
            var token = apiResponse(loginDtos).Result;
            if (token != "")
            {

                var order = new Order();
                order.OrderNumber = "A" + (new Random()).Next(11111, 99999).ToString();
                order.Total = (decimal?)cart.TotalPrice();
                order.OrderDate = DateTime.Now;
                order.OrderState = false;
                order.Username = User.Identity.Name;

                order.AdresTanimi = details.AdresTanimi;
                order.Adres = details.Adres;
                order.Sehir = details.Sehir;
                order.Semt = details.Telefon;

                foreach (var product in cart.Products) //kartın içindeki ürünler
                {
                    var orderLine = new OrderLine();
                    orderLine.Quantity = product.Quantity;
                    orderLine.Price = product.Product.Price;
                    orderLine.ProductId = product.Product.ProductId;

                    order.OrderLines.Add(orderLine);

                }
                var result1 = saveOrder(token, order).Result;
               // var result2 = saveOrderLine(token, order).Result;
                //var result= result1 && result2;
                return result1;


            }
            return false;

        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }


        private Cart GetCart()
        {
            return HttpContext.Session.GetObjectFromJson<Cart>("Cart") ?? new Cart();

        }

        public async Task<ProductDetailsModel> getDetail(string token, int id)
        {
            var responseList = new ProductDetailsModel();

            var endPoint = "http://localhost:26144/api/products/getbyid?productId=" + id.ToString();
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var httpClient = new HttpClient(clientHandler);
            //var token = HttpContext.Session.GetString("Token");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            using (var response = await httpClient.GetAsync(endPoint))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    responseList = JsonConvert.DeserializeObject<ProductDetailsModel>(apiResponse);
                }


            }
            return responseList;

        }

        public async Task<bool> saveOrder(string token, Order order)
        {
            var responseList = new Order();

            var endPoint = "http://localhost:26144/api/Orders/add";
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var httpClient = new HttpClient(clientHandler);
            //var token = HttpContext.Session.GetString("Token");
            StringContent Tokencontent = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            using (var response = await httpClient.PostAsync(endPoint, Tokencontent))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    // responseList = JsonConvert.DeserializeObject<Order>(apiResponse);
                    return true;
                }


            }
            return false;

        }

        public async Task<bool> saveOrderLine(string token, Order order)
        {
            var responseList = new Order();

            var endPoint = "http://localhost:26144/api/Orders/addOrderLine";
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var httpClient = new HttpClient(clientHandler);
            //var token = HttpContext.Session.GetString("Token");
            StringContent Tokencontent = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            using (var response = await httpClient.PostAsync(endPoint, Tokencontent))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    // responseList = JsonConvert.DeserializeObject<Order>(apiResponse);
                    return true;
                }


            }
            return false;

        }

        private async Task<string> apiResponse(UserForLoginDto loginDtos)
        {
            loginDtos.Email = "s@gmail.com";
            loginDtos.Password = "123";

            var endPoint = "http://localhost:26144/api/auth/login";
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var httpClient = new HttpClient(clientHandler);
            StringContent Tokencontent = new StringContent(JsonConvert.SerializeObject(loginDtos), Encoding.UTF8, "application/json");
            AccessToken tokenResponse = new AccessToken();
            using (var response = await httpClient.PostAsync(endPoint, Tokencontent))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tokenResponse = JsonConvert.DeserializeObject<AccessToken>(apiResponse);
                    return tokenResponse.Token;
                }
                else
                {
                    return "";

                }

            }
        }
    }
}
