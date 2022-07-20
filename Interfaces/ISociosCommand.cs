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
        Task<IEnumerable<ResultSearchSocios>> getSocioSearch(SocioSearch socioSearch);
        Task<IEnumerable<ResultSearchSocioProduct>> getSocioProductSearch(SocioProductSearch socioProduct);
        Task<int> updateStatusSocio(int id, int status);
        Task<IEnumerable<SocioHistory>> getSocioHistory(int id);
        Task<SocioDetail> getSocioDetail(int id);
    }
}
