using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.Prospectos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IProspectosRepository
    {
        Task<Prospectos_documentacion> getDocumentById(int id);

        Task<bool> updateProspectoValidacion(Validacionupdate prospectoValidacion);



    }
}
