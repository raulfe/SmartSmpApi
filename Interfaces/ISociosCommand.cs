using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.DTOs;
using SmartBusinessAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface ISociosCommand
    {
        Task<Socios> getSocioById(int id);
        Task<IEnumerable<KycSociosList>> getSociosValidacion();
        Task<Socios> getSocioByEmail(string email);
        Task<Validacion> getVerified(int id);
        Task<SocioInfo> getInfoById(int id);
        Task<SocioValidacion> getValidationById(int id);
        Task<IEnumerable<SocioValidacion>> getValidationesById(int id);
        Task<SociosR> getSocioByPosition(int position);
        Task<CountType> getSociosCount();

    }
}
