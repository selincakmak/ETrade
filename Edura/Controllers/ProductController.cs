using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Business.Utilities.Jwt;
using Entities.Concrete;
using Entities.Response.Product;
using Newtonsoft.Json;
using Entities.Response;

namespace Edura.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 2; //güncelle


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(string category, int page = 1)
        {
            UserForLoginDto loginDtos = new UserForLoginDto();
            var token = apiResponse(loginDtos).Result;
            if (token != "")
            {
                var products = getAll(token, category).Result;
                var count = products.Count();
                if (!string.IsNullOrEmpty(category))
                {
                    products = getAll(token, category).Result;
                }
                products = products.Skip((page - 1) * PageSize).Take(PageSize).ToList();


                var model = new ProductListModel()
                {
                    Products = products,
                    PagingInfo = new PagingInfo()
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = count

                    }

                };

                return View(model);
            }
            else
            {
                List<Product> aa = new List<Product>();

                return View(aa);
            }
        }
        public IActionResult Details(int id)
        {
            UserForLoginDto loginDtos = new UserForLoginDto();
            var token = apiResponse(loginDtos).Result;
            if (token != "")
            {
                var result = getDetail(token, id).Result;
                return View(result);
            }
            else
            {
                ProductDetailsModel aa = new ProductDetailsModel();

                return View(aa);
            }
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

        public async Task<List<Product>> getAll(string token, string category)
        {
            string endPoint = "";
            var responseList = new List<Product>();
            if (!string.IsNullOrEmpty(category))
                endPoint = "http://localhost:26144/api/Products/getall?categoryName="+ category;
            else
                endPoint = "http://localhost:26144/api/products/getall";
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
                    responseList = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }


            }
            return responseList;

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
