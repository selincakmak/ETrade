using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Edura.Helper
{
    public class AdminAuthorizationHelper:AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly HttpClient _httpClient;
        public AdminAuthorizationHelper()
        {
            _httpClient = new HttpClient();
        }
        public readonly string Admin = "Admin"; //farklı roller için dizi tanımlayıp ordan alabilirsin,roller operation claimde
        string signInPageUrl = "/Account/Login";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.FindFirst(ClaimTypes.Role) != null)
            {
                var role = context.HttpContext.User.FindFirst(ClaimTypes.Role).Value;

                if (role != Admin)
                {
                    context.Result = new RedirectResult(signInPageUrl);
                }
            }
            else
            {
                context.Result = new RedirectResult(signInPageUrl);
            }
            
        }
    }
}
