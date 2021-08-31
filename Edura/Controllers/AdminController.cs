using Business.Utilities.Jwt;
using Edura.Models;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Edura.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(Category entity)
        {
          UserForLoginDto loginDtos = new UserForLoginDto();
            var token = apiResponse(loginDtos).Result;
            if (token != "")
            {
                if (ModelState.IsValid)
                {
                    addCategory(token, entity); 
                    return Ok(entity);

                }
                            
            }
            return BadRequest();
        }

        [HttpGet]
    
        public IActionResult AddProduct(Category entity)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> AddProduct(Product entity,IFormFile file)
        {
            UserForLoginDto loginDtos = new UserForLoginDto();
            var token = apiResponse(loginDtos).Result;
            if (token != "")
            {
                if (ModelState.IsValid)
                {
                    if(file != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\products", file.FileName);
                        //var path = Path.Combine(Directory.GetCurrentDirectory(), "~/edura/images/products", file.FileName); 
                        using (var stream = new FileStream(path, FileMode.Create))
                        {

                            await file.CopyToAsync(stream);
                            entity.Image = file.FileName;
                        }
                    }

                    entity.DateAdded = DateTime.Now;
                    addProduct(token, entity);
                    return RedirectToAction("CatalogList");
                }

                return View(entity);

            }
            return View();
        }
        public IActionResult CatalogList()
        {
            UserForLoginDto loginDtos = new UserForLoginDto();
            var token = apiResponse(loginDtos).Result;
            if (token != "")
            {
                var model = new CatalogListModel()
                {
                    Categories = getCategories(token).Result,
                    Products = getProducts(token).Result


                };

                return View(model);
            }
            return View();
        }
        [HttpGet]
        public IActionResult EditCategory(int id) {

            UserForLoginDto loginDtos = new UserForLoginDto();
            var token = apiResponse(loginDtos).Result;
            if (token != "")
            {
                var entity = getDetail(token, id).Result;
              
                return View(entity);
            }
            return View();
        }
        [HttpPost]
        public IActionResult EditCategory(Category entity) {
            UserForLoginDto loginDtos = new UserForLoginDto();
            var token = apiResponse(loginDtos).Result;
            if (token != "")
            {
                if (ModelState.IsValid)
                {
                    editCategory(token, entity);
                    return RedirectToAction("CatalogList");    
                }

               
            }
            return View("Error");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCategory(int ProductId,int CategoryId)
        {
            UserForLoginDto loginDtos = new UserForLoginDto();
            var token = apiResponse(loginDtos).Result;
            if (token != "")
            {
                if (ModelState.IsValid)
                {
                    removeCategory(token, ProductId, CategoryId);
                    return Ok();
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult DeleteCategory(int CategoryId)
        {
            UserForLoginDto loginDtos = new UserForLoginDto();
            var token = apiResponse(loginDtos).Result;
            if (token != "")
            {
                if (ModelState.IsValid)
                {
                    deleteCategory(token,CategoryId);
                    return Ok();
                    
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult DeleteProduct(int ProductId)
        {
            UserForLoginDto loginDtos = new UserForLoginDto();
            var token = apiResponse(loginDtos).Result;
            if (token != "")
            {
                if (ModelState.IsValid)
                {
                    deleteProduct(token, ProductId);
                    return Ok();

                }
                return BadRequest();
            }
            return BadRequest();
        }
        public async Task<AdminEditCategoryModel> getDetail(string token, int id)
        {
            var responseList = new AdminEditCategoryModel();

            var endPoint = "http://localhost:26144/api/categories/getbyid?categoryId=" + id.ToString();
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
                    responseList = JsonConvert.DeserializeObject<AdminEditCategoryModel>(apiResponse);
                }


            }
            return responseList;

        }
        public async Task<List<Product>> getProducts(string token)
        {
            var responseList = new List<Product>();

            var endPoint = "http://localhost:26144/api/products/getall";
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
        public async Task<List<Category>> getCategories(string token)
        {
            var responseList = new List<Category>();

            var endPoint = "http://localhost:26144/api/categories/getall";
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
                    responseList = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
                }


            }
            return responseList;

        }
        public async Task<Category> addCategory(string token,Category category)
        {
            var responseList = new Category();

            var endPoint = "http://localhost:26144/api/categories/add";
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var httpClient = new HttpClient(clientHandler);
            //var token = HttpContext.Session.GetString("Token");
            StringContent Tokencontent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            using (var response = await httpClient.PostAsync(endPoint, Tokencontent))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                     responseList = JsonConvert.DeserializeObject<Category>(apiResponse);
                    return responseList;
                }


            }
            return responseList;

        }
        public async Task<Category> removeCategory(string token, int ProductId, int CategoryId)
        {
            var responseList = new Category();

            var endPoint = "http://localhost:26144/api/Categories/delete?productId="+ProductId+"&categoryId="+CategoryId;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var httpClient = new HttpClient(clientHandler);
            //var token = HttpContext.Session.GetString("Token");
            StringContent Tokencontent = new StringContent(JsonConvert.SerializeObject(ProductId, (Formatting)CategoryId), Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            using (var response = await httpClient.PostAsync(endPoint, Tokencontent))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    responseList = JsonConvert.DeserializeObject<Category>(apiResponse);
                    return responseList;
                }


            }
            return responseList;

        }

        public async Task<Category> deleteCategory(string token, int CategoryId)
        {
            var responseList = new Category();

            var endPoint = "http://localhost:26144/api/Categories/deleteCategory?categoryId=" + CategoryId;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var httpClient = new HttpClient(clientHandler);
            //var token = HttpContext.Session.GetString("Token");
            StringContent Tokencontent = new StringContent(JsonConvert.SerializeObject(CategoryId, (Formatting)CategoryId), Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            using (var response = await httpClient.PostAsync(endPoint, Tokencontent))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    responseList = JsonConvert.DeserializeObject<Category>(apiResponse);
                    return responseList;
                }


            }
            return responseList;

        }

        public async Task<Product> deleteProduct(string token, int ProductId)
        {
            var responseList = new Product();

            var endPoint = "http://localhost:26144/api/Products/deleteProduct?productId=" + ProductId;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var httpClient = new HttpClient(clientHandler);
            //var token = HttpContext.Session.GetString("Token");
            StringContent Tokencontent = new StringContent(JsonConvert.SerializeObject(ProductId), Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            using (var response = await httpClient.PostAsync(endPoint, Tokencontent))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    responseList = JsonConvert.DeserializeObject<Product>(apiResponse);
                    return responseList;
                }


            }
            return responseList;

        }
        public async Task<Category> editCategory(string token, Category category)
        {
            var responseList = new Category();

            var endPoint = "http://localhost:26144/api/categories/update";
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var httpClient = new HttpClient(clientHandler);
            //var token = HttpContext.Session.GetString("Token");
            StringContent Tokencontent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            using (var response = await httpClient.PostAsync(endPoint, Tokencontent))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    responseList = JsonConvert.DeserializeObject<Category>(apiResponse);
                    return responseList;
                }


            }
            return responseList;

        }
        public async Task<Product> addProduct(string token, Product product)
        {
            var responseList = new Product();

            var endPoint = "http://localhost:26144/api/products/add";
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var httpClient = new HttpClient(clientHandler);
            //var token = HttpContext.Session.GetString("Token");
            StringContent Tokencontent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            using (var response = await httpClient.PostAsync(endPoint, Tokencontent))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    responseList = JsonConvert.DeserializeObject<Product>(apiResponse);
                    return responseList;
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

