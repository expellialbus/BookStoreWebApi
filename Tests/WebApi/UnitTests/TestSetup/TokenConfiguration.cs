using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace UnitTests.TestSetup
{
    public class TokenConfiguration : IConfiguration
    {
        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new System.NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new System.NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new System.NotImplementedException();
        }

        public string this[string key]
        {
            get => Get(key);
            set => throw new System.NotImplementedException();
        }

        private string Get(string key)
        {
            switch (key)
            {
                case "Audience":
                    return "www.test.com";
                
                case "Issuer":
                    return "www.test.com";
                
                case "SecurityKey":
                    return "This is my secret key for authentication";
                
                default:
                    return null;
            }
        }
    }
}