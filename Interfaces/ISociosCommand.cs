using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.KycValidacion;
using SmartBusinessAPI.Entities.ValidationStatus;
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
        Task<SociosR> getSocioByIdInfo(int id);
        Task<Socios> getSocioByEmail(string email);
        Task<CountType> getSociosCount();
        Task<SociosR> getSocioByPosition(int position);
        Task<IEnumerable<KycSociosList>> getSociosValidacion();
        Task<SocioData> getSocioByEmailAuth(string email);
        Task<SocioMeta> getSocioByEmailMeta(string email);
        Task<IEnumerable<SocioValidacion>> getValidationesById(int id);
        Task<SocioInfo> getInfoById(int id);
        Task<Socios_documentacion> getDocumentById(int id);
        Task<SocioValidacionR> getLastValidacionById(int id);
        Task<SocioValidacionR> getSocioValidacionbyValidacion(int validacionId);
        Task<int> insertSocioValidacion(SocioValidacion socioValida);
        Task<int> updateSocioIdValidacion(int validacion, int socio);
    }
}
