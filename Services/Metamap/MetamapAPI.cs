using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartBusinessAPI.Entities.Verification;
using SmartBusinessAPI.Exceptions;
using SmartBusinessAPI.Models.Metamap;
using SmartBusinessAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Services.Metamap
{
    public class MetamapAPI : IMetamapAPI
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MetamapAPI> _logger;
        const string BASE = "Metamap:Services:Base";
        const string AUTH = "/oauth";
        const string VERIFY = "/v2/verifications/";
        const string CLIENT = "Metamap:Client_id";
        const string SECRET = "Metamap:Client_secret";
        public MetamapAPI(IConfiguration configuration, ILogger<MetamapAPI> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<Auth> extractToken()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var authenticationString = $"{_configuration.GetSection(CLIENT).Value}:{_configuration.GetSection(SECRET).Value}";
                    var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                    var dict = new Dictionary<string, string>();
                    dict.Add("grant_type", "client_credentials");
                    var req = new HttpRequestMessage(HttpMethod.Post, _configuration.GetSection(BASE).Value + AUTH) { Content = new FormUrlEncodedContent(dict) };
                    var response = await client.SendAsync(req);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var auth = JsonConvert.DeserializeObject<Auth>(data);
                        return auth;

                    }
                    else
                    {
                        throw new BusinessException("El servicio de metamap fallo");
                    }

                }
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                throw new BusinessException(e.Message);
            }

        }

        public async Task<VerificationObject> getMetamapDataById(string id,string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = _configuration.GetSection(BASE).Value + VERIFY + id;
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var verify = JsonConvert.DeserializeObject<VerificationObject>(data);
                        return verify;

                    }
                    else
                    {
                        throw new BusinessException("El servicio de metamap fallo");
                    }
                }
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                throw new BusinessException(e.Message);
            }
            
        }

        public async Task<VerificationObject> getMetamapDataByUrl(string url, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var verify = JsonConvert.DeserializeObject<VerificationObject>(data);
                        return verify;

                    }
                    else
                    {
                        throw new BusinessException("El servicio de metamap fallo");
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
