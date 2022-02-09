using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Applications.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Token Handle()
        {
            // Checks refresh token and its expire date time
            // refresh token's expire date should be greater than now
            // to be a valid token
            var user = _context.Users.SingleOrDefault(
                user => user.RefreshToken == RefreshToken && user.RefreshTokenExpireDate > DateTime.Now);

            // if refresh token invalid or expired
            // an exception will be thrown
            if (user is null)
                throw new InvalidOperationException("Invalid refresh token.!");

            TokenHandler handler = new TokenHandler(_configuration);
            Token token = handler.CreateAccessToken();

            user.RefreshToken = token.RefreshToken;
            
            // Sets RefreshTokenExpireDate to 5 minutes later of AccessTokenExpireDate 
            // This time span will be used to get a new access token
            user.RefreshTokenExpireDate = token.AccessTokenExpireDate.AddMinutes(5);

            _context.SaveChanges();
            
            return token;
        }
    }
}