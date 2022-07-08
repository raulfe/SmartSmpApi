using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.SociosDocument;
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
        Task<int> updateSocioValidacion(SocioValidacion socioValida);
        Task<SocioValidacion> getLastValidation(int validacion);

        Task<DataSocioDocumentacion> getDataSocioDocument(int socio);


    }
}
