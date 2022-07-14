using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Entities.ValidationStatus;
using SmartBusinessAPI.Exceptions;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ILogger<LoginRepository> _logger;
        private readonly ISociosCommand _socios;
        private readonly IProspectosCommand _prospecto;
        public LoginRepository(
            ILogger<LoginRepository> logger, 
            ISociosCommand socios,
            IProspectosCommand prospecto
            )
        {
            _logger = logger;
            _socios = socios;
            _prospecto = prospecto;
        }

        public async Task<int> emailValidation(string email, int padre)
        {
            if (!IsValidEmail(email))
            {
                throw new BusinessException("Email no valido");
            }
            try
            {
                var soc = await _socios.getSocioByEmail(email);
                if (soc != null)
                {
                    _logger.LogInformation("El usuario ya es un socio");
                    return 0;
                }
                var data = await _prospecto.getProspectoByEmail(email);
                if (data != null)
                {
                    if (data.Tipo == 1)
                    {
                        _logger.LogInformation("El usuario esta interesado pero no ah completado el registro");
                        _logger.LogInformation("Redireccionando a login");
                        return 1;
                    }
                    else if (data.Tipo == 2)
                    {
                        _logger.LogInformation("El usuario ya es un  prospecto");
                        _logger.LogInformation("Redireccionando a login");
                        return 2;
                    }
                    else
                    {
                        throw new BusinessException("Tipo no valido");
                    }
                }
                else
                {

                    _logger.LogInformation("El usuario no existe, creando interesado");
                    Prospectos prospecto = new Prospectos()
                    {
                        Email = email,
                        Padre = padre,
                        Tipo = 1,
                        Email_Verified = false,
                        Last_Login = DateTime.Now
                    };
                    var response = await _prospecto.insertInteresado(prospecto);
                    if (response > 0)
                    {
                        _logger.LogInformation($"Interesado ({email}) creado satisfactoriamente");
                        return 3;
                    }
                    else
                    {
                        throw new BusinessException("La informacion del padre no existe");
                    }
                    
                }
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                throw new BusinessException(e.Message);
            }

        }

        public JwtSecurityToken decodeToken(string token)
        {
            try
            {
                var stream = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                return tokenS;
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                throw new BusinessException(e.Message);
            }

        }

        public async Task<object> getValidation(string email)
        {
            var data = await _prospecto.getProspectoByEmailAuth(email);
            var data2 = await _socios.getSocioByEmailAuth(email);
            if (data == null && data2 == null)
            {
                _logger.LogError("El prospecto no existe");
                throw new BusinessException("El prospecto no existe");
            }else if(data != null)
            {
                return data;
            }
            else
            {
                return data2;
            }
            
        }

        public async Task<object> getMetaValidation(string email)
        {
            var data = await _prospecto.getProspectoByEmailMeta(email);
            var data2 = await _socios.getSocioByEmailMeta(email);
            if(data == null && data2 == null)
            {
                _logger.LogError("El socio no existe");
                throw new BusinessException("El socio no existe");
            }
            else if (data != null)
            {
                return data;
            }
            else
            {
                return data2;
            }
        }

        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
