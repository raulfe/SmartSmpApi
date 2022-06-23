using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Exceptions;
using SmartBusinessAPI.Services.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Services.Auth0
{
    public class AuthAPI : IAuthAPI
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthAPI> _logger;
        const string BASE = "Auth0:base";
        const string AUTH = "/oauth/token";
        const string GRANT = "Auth0:grant_type";
        const string CLIENT = "Auth0:client_id";
        const string SECRET = "Auth0:client_secret";
        const string AUDIENCE = "Auth0:audience";
        const string PROSP = "auth0|prospecto|";
        const string VERIFY = "/api/v2/jobs/verification-email";
        public AuthAPI(IConfiguration configuration, ILogger<AuthAPI> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<AuthR> extractToken()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    var payload = new
                    {
                        grant_type = _configuration.GetSection(GRANT).Value,
                        client_id = _configuration.GetSection(CLIENT).Value,
                        client_secret = _configuration.GetSection(SECRET).Value,
                        audience = _configuration.GetSection(AUDIENCE).Value
                    };
                    var serialize = JsonConvert.SerializeObject(payload);
                    HttpContent content = new StringContent(serialize, Encoding.UTF8, "application/json");
                    var url = $"{_configuration.GetSection(BASE).Value}{AUTH}";
                    var response = await client.PostAsync(url,content);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var auth = JsonConvert.DeserializeObject<AuthR>(data);
                        return auth;

                    }
                    else
                    {
                        throw new BusinessException("El servicio de auth0 fallo");
                    }

                }
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                throw new BusinessException(e.Message);
            }

        }

        public async Task<bool> resendEmail(string token,int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    var payload = new
                    {
                        user_id = $"{PROSP}{id}",
                    };
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                    var serialize = JsonConvert.SerializeObject(payload);
                    HttpContent content = new StringContent(serialize, Encoding.UTF8, "application/json");
                    var url = $"{_configuration.GetSection(BASE).Value}{VERIFY}";
                    var response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;

                    }
                    else
                    {
                        throw new BusinessException("El servicio de auth0 fallo");
                    }

                }
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                throw new BusinessException(e.Message);
            }
        }
    }
}
