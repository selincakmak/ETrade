using Business.Utilities.Jwt;
using Entities.Dtos;
using Entities.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Edura.Controllers
{
   // [Authorize]
    public class AccountController : Controller
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email,string password, string returnUrl)
        {
            UserForLoginDto loginDtos = new UserForLoginDto();
            loginDtos.Email = email;
            loginDtos.Password = password;
            var token = apiResponse(loginDtos).Result;
            if (token.Token != "")
            {
                var userclaim = token.claim.ToArray();
          
                HttpContext.Session.SetString("id", userclaim[0].ToString());
                HttpContext.Session.SetString("email", userclaim[1].ToString());
                HttpContext.Session.SetString("username", userclaim[2].ToString());
                HttpContext.Session.SetString("role", userclaim[3].ToString());


                var adminClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, userclaim[0].ToString()),
                        new Claim(ClaimTypes.Email, userclaim[1].ToString()),
                        new Claim(ClaimTypes.Name, userclaim[2].ToString()),
                        new Claim(ClaimTypes.Role, userclaim[3].ToString()),
                    };
                var adminIdentity = new ClaimsIdentity(adminClaims, "AdminPassaport");
                var userPrincipal = new ClaimsPrincipal(new[] { adminIdentity });

                await HttpContext.SignInAsync(userPrincipal);


                return Redirect(returnUrl ?? "/");
            }

            HttpContext.Session.SetString("id", "0");

           // HttpContext.SignInAsync()
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        private async Task<AccessToken> apiResponse(UserForLoginDto loginDtos)
        {
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
                    return tokenResponse;
                }
                else
                {
                    return tokenResponse;

                }

            }
        }
    }
}
