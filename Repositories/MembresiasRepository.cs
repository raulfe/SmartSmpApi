using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.NuevaMembresia;
using SmartBusinessAPI.Exceptions;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Repositories
{
    public class MembresiasRepository : IMembresiasRepository
    {
        private readonly ILogger<MembresiasRepository> _logger;
        private readonly IMembresiasCommand _membresias;
        public MembresiasRepository(ILogger<MembresiasRepository> logger, IMembresiasCommand membresias)
        {
            _logger = logger;
            _membresias = membresias;
        }

        public async Task<IEnumerable<Membresias>> GetMembresias(MembresiasFilter filter)
        {
            var data = await _membresias.getMembresias();
            if (data == null || data.Count() == 0)
            {
                throw new BusinessException("No hay informacion en la base de datos");
            }
            if (filter.Nombre != null)
            {
                data = data.Where(x => x.Nombre == filter.Nombre).ToList();
            }
            if (filter.Estado != null)
            {
                data = data.Where(x => x.Activo == filter.Estado).ToList();
            }
            return data;

        }

        public async Task<bool> processNewMembership(NewMembership membresia)

        {
            var nameValidation = await _membresias.getMembresia(membresia.Membresia.Nombre);
            if (nameValidation != null)
            {
                _logger.LogError("El nombre ya ha sido usado por otra Membresia");
                throw new BusinessException("El nombre ya ha sido usado por otra Membresia");
            }
            if (membresia.Paises.Count() == 0)
            {
                _logger.LogError("La Membresia debe contener al menos un pais donde aplicarse");
                throw new BusinessException("La Membresia debe contener al menos un pais donde aplicarse");

            }


            var resInsert = await _membresias.insertNewMembership(membresia.Membresia);
            if (resInsert == 0)
            {
                _logger.LogError("La Membresia no pudo ser registrada");
                throw new BusinessException("La Membresia no pudo ser registrada");
            }

            _logger.LogInformation("Mebresia registrada satisfactoriamente");
            var newMemb = await _membresias.getMembresia(membresia.Membresia.Nombre);
            var idMembresia = newMemb.Membresia;
            foreach (var pais in membresia.Paises)
            {
                var resPais = await _membresias.insertMembresiaPais(pais, idMembresia);
                if (resPais == 0)
                {
                    _logger.LogError($"Error al insertar la Membresia {idMembresia} con el pais {pais.Pais}");
                    continue;
                }
            }
            return true;

        }


        public async Task<bool> updateMembresia(updateMembership updateMembership) 
        {
            var codMembresia = await _membresias.getMembresiaById(updateMembership.membresia.Membresia);
            if (codMembresia == null)
            {
                _logger.LogError("La Membresia no existe");
                throw new BusinessException("La Membresia no existe");
            }
            if ( updateMembership.Paises.Count() == 0) 
            {
                _logger.LogError("La Membresia debe contener al menos un país donde aplicarse ");
                throw new BusinessException(" La Mebresia debe contener al menos un pais donde aplicarse");
            }
            if (updateMembership.membresia == null) 
            {
                _logger.LogError("La Membresia no debe contener campos vacios");
                throw new BusinessException(" La Membresia no debe contener campos vacios");
            }
            var resUpdate = await _membresias.updateMembership(updateMembership.membresia);
            if (resUpdate == 0) 
            {
                _logger.LogError("La Membresia no pudo ser actualizada");
                throw new BusinessException("La Membresia no pudo ser actualizada");
            }
            foreach (var pais in updateMembership.Paises) 
            {
                var resPais = await _membresias.updateMembresiaPaises(pais, updateMembership.membresia.Membresia);
                if (resPais == 0) 
                {
                    _logger.LogError($"Error al actualizar la membresia {updateMembership.membresia.Membresia} con el pais {pais.Pais}");
                    continue;
                }

            }

            return true;

        }

    }
}
