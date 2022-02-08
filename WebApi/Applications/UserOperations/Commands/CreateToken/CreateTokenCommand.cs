using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Applications.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenDtoModel Model { get; set; }

        private readonly IConfiguration _configuration;
        private readonly IBookStoreDbContext _context;

        public CreateTokenCommand(IConfiguration configuration, IBookStoreDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public Token Handle()
        {
            // Checks for user presence
            var user = _context.Users
                .SingleOrDefault(user => user.Email == Model.Email && user.Password == Model.Password);

            // An exception will be thrown 
            // if there is no such user in the database
            if (user is null)
                throw new InvalidOperationException("Email or Password is wrong.!");
            
            TokenHandler handler = new TokenHandler(_configuration);
            Token token = handler.CreateAccessToken(); // creates access token

            user.RefreshToken = token.RefreshToken;
            
            // Sets RefreshTokenExpireDate to 5 minutes later of AccessTokenExpireDate 
            // This time span will be used to get a new access token
            user.RefreshTokenExpireDate = token.AccessTokenExpireDate.AddMinutes(5);

            _context.SaveChanges();

            return token;
        }
    }

    public class CreateTokenDtoModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}