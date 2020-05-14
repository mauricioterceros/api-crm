using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using BackingServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CRM_CLIENTS.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenticationMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly IUsersDB _iuserDB;

        public AuthenticationMiddleware(RequestDelegate next, IUsersDB usersDB)
        {
            _next = next;
            _iuserDB = usersDB;
        }

        public Task Invoke(HttpContext httpContext)
        {

            string var = httpContext.Request.Headers["Authorization"].ToString();
            // var = "mateo.lopez<3:Pass123"
            Console.WriteLine("This is the Auth Middleware");
            Console.WriteLine("Connecting with FB API (oAuth)"); // SSO, oAuth2.0/3.0
            Console.WriteLine("waiting for FB credentials");
            Console.WriteLine("optional: store JWT token to our db (through businness logic)");
            Console.WriteLine("GRANTING ACCESS TO THE SYSMTEM");
            // Consult to busines logic
            // Consutl to DB if user exists or not (user/pass)
            // HTTP Header / Authorization: Basic base64(user:pass)
            // GRANT Acess
            string user = var.Split(':')[0];
            string pass = var.Split(':')[1];


            if (_iuserDB.UserExists(user, pass))
            {
                return _next(httpContext);
            }
            else
            {
                throw new AuthenticationException("Unauthorized Access, Username Or password Invalid");
            }



            //else 
            //  throw new unauthorized 
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
