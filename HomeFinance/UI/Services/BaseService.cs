using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UI.Services
{
    public class BaseService
    {
        public readonly HttpClient _client;
        private IConfiguration _confstring;
        public BaseService(HttpClient client)
        {
            _client = client;
            _confstring = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var uri = new Uri(_confstring["ServiceUrl"]);
            if (_client.BaseAddress!= uri)
                _client.BaseAddress = uri;
        }
    }
}
