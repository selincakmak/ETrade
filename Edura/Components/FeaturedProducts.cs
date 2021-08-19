﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Business.Utilities.Jwt;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Edura.Components
{
    public class FeaturedProducts:ViewComponent
    {


        public IViewComponentResult Invoke()
        {
            UserForLoginDto loginDtos = new UserForLoginDto();
            var token = apiResponse(loginDtos).Result;
            if (token != "")
            {
                var result = getAll(token).Result;
                return View(result.Where(x => x.IsFeatured.Value));
            }
            else
            {
                List<Product> aa = new List<Product>();

                return View(aa);
            }
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
        public async Task<List<Product>> getAll(string token)
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

    }
}
