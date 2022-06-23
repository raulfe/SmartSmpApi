using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Repositories
{
    public class Auth0Repository : IAuth0Repository
    {
        private readonly IAuthAPI _auth;
        private readonly ILogger<Auth0Repository> _logger;

        public Auth0Repository(IAuthAPI auth, ILogger<Auth0Repository> logger)
        {
            _auth = auth;
            _logger = logger;
        }

        public async Task<bool> ReSendValidation(int id)
        {
            var data = await _auth.extractToken();
            var result = await _auth.resendEmail(data.access_token, id);
            return result;
        }
    }
}
