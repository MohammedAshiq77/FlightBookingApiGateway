using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Helper
{
   public class jwtTokenManager
    {
        public string CreateToken(string EmailId, string PassWord)
        {
            var tokenString = "";
            try
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("myEncryptionKey@143#"));

                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                  {
                 new Claim(ClaimTypes.Name,  EmailId + "," + PassWord)
                 };
                var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:59329",
                audience: "http://localhost:59329",
                claims: claims,
                notBefore: DateTime.Now,
                signingCredentials: signinCredentials
                );
                tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            }
            catch (Exception e) { }

            return tokenString;
        }
    }
}
