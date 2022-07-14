using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.Socios;
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
        Task<Socios> getSocioByEmail(string email);
        Task<CountType> getSociosCount();
        Task<SociosR> getSocioByPosition(int position);
        Task<Socios_documentacion> getDocumentById(int id);
        Task<int> updateSocioIdValidacion(int validacion, int socio);
        Task<SocioValidacionR> getLastValidacionById(int id);
        Task<SocioValidacionR> getSocioValidacionbyValidacion(int validacionId);
        Task<int> insertSocioValidacion(SocioValidacion socioValida);
    }
}
