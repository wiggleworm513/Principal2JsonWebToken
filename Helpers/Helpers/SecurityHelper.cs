using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Helpers
{
    public static class SecurityHelper
    {
        public static string GetAuthToken()
        {
            IPrincipal User = AppHelper.User;
            IConfiguration configuration = AppHelper.Config;

            //Setup claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.Identity.Name),
                new Claim(ClaimTypes.Authentication, User.Identity.IsAuthenticated.ToString())
            };

            //"GP_AUD_CAMA_VIEW"
            //"GP_AUD_CAMA_EDIT_PAST"
            //"GP_AUD_CAMA_EDIT_FUTURE"
            //"GP_AUD_CAMA_ADMINISTRATOR"            

            //if (Common.Utilities.UserUtilities.IsInGroup(User.Identity.Name, "GP_AUD_CAMA_VIEW"))
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, "ReadOnly"));
            //}

            //if (Common.Utilities.UserUtilities.IsInGroup(User.Identity.Name, "GP_AUD_CAMA_EDIT_PAST"))
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, "EditPast"));
            //}

            //if (Common.Utilities.UserUtilities.IsInGroup(User.Identity.Name, "GP_AUD_CAMA_EDIT_FUTURE"))
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, "EditFuture"));
            //}

            //if (Common.Utilities.UserUtilities.IsInGroup(User.Identity.Name, "GP_AUD_CAMA_ADMINISTRATOR"))
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            //}

            //Read signing symmetric key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Create a token
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Issuer"],
                claims: claims.ToArray(),
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            //Return signed JWT token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

