using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }

        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken()
        {
            // Creates a token model
            Token model = new Token();
    
            // Defines key type
            SymmetricSecurityKey key =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            
            // Creates credentials with the provided security algorithm 
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            model.AccessTokenExpireDate = DateTime.Now.AddMinutes(15);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: model.AccessTokenExpireDate,
                notBefore: DateTime.Now, // Sets when token will be active
                signingCredentials: credentials
            );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            // Creates token
            model.AccessToken = handler.WriteToken(token); 
            model.RefreshToken = CreateRefreshToken();

            return model;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}