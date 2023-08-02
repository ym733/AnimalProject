using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Animal.WebAPI.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {   // checks if the request has an authorization header
                return Task.FromResult(AuthenticateResult.Fail("Authorization Key Missing"));
            }
            
            var AuthorizationHeader = Request.Headers["Authorization"].ToString();

            if (!AuthorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {   // checks if the authorization header starts with 'Basic ' meaning the authorization is a basic authorization
                return Task.FromResult(AuthenticateResult.Fail("Authorization Header does not Have a Type (Roles)"));
            }
            
            var authBase64Decoded = Encoding.UTF8.GetString(
                Convert.FromBase64String(
                    AuthorizationHeader.Replace("Basic ", "", StringComparison.OrdinalIgnoreCase)
                )   
            );

            var authSplit = authBase64Decoded.Split(new[] { ':' }, 2); 
            // the format of the authorization header will be: {user name}:{user secret}:{ID}
            // so when we split it by the ':' we get an array where the [0] is the user name and [1] is the user secret

            if( authSplit.Length != 2 )
            {   // checks if the header is formatted correctly
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header format"));
            }
            
            var userName = authSplit[0];
            var userSecret = authSplit[1];

            //if( userName != "test" || userSecret != "testing" )
            //{   // this is just a prototype but normally here we check if the userSecret matches the userSecret in database
            //    return Task.FromResult(AuthenticateResult.Fail("Password is incorrect"));
            //}


            //check DB if this Username and password are correct ? will return User Data: error message;
            using var obj = new AnimalProvider.Users();
            Entities.User? DBobject = obj.getUserByInfo(userName, userSecret);

            if(DBobject == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("UserName or Password is incorrect"));
            }

            var client = new BasicAuthenticationClient
            {
                AuthenticationType = BasicAuthenticationDefaults.AuthenticationScheme,
                IsAuthenticated = true,
                Name = userName
            };

            var varClaims = new ClaimsPrincipal(new ClaimsIdentity(client, new[]
            {
                //new Claim("id", DBobject.Id),
                new Claim(ClaimTypes.Name, DBobject.Name),
                new Claim(ClaimTypes.Email, DBobject.Email),
                new Claim(ClaimTypes.DateOfBirth, DBobject.DateOfBirth),
                new Claim(ClaimTypes.Role, DBobject.role)
            }));
            
            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(varClaims, Scheme.Name)));
        }
    }
}
