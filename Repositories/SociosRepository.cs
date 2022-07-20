﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.Socios;
using SmartBusinessAPI.Exceptions;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Repositories
{
    public class SociosRepository : ISociosRepository
    {
        private readonly ISociosCommand _socios;
        private readonly ILogger<SociosRepository> _logger;

        public SociosRepository(ISociosCommand socios, ILogger<SociosRepository> logger)
        {
            _logger = logger;
            _socios = socios;
        }

        public async Task<Socios> getSocioByID(int id)
        {
            try
            {
                var data = await _socios.getSocioById(id);
                return data;
            }
            catch (Exception e)
            {

                throw new BusinessException($"Exception found: {e.Message}");
            }

        }

        public async Task<SociosR> getSocioByPosition(int id)
        {
            try
            {
                var data = await _socios.getSocioByPosition(id);
                return data;
            }
            catch (Exception e)
            {

                throw new BusinessException($"Exception found: {e.Message}");
            }
        }

        public async Task<CountType> getSociosCount()
        {
            try
            {
                var data = await _socios.getSociosCount();
                return data;
            }
            catch (Exception e)
            {

                throw new BusinessException($"Exception found: {e.Message}");
            }
        }

        public async Task<Socios_documentacion> getDocumentById(int id)
        {
            try
            {
                var data = await _socios.getDocumentById(id);
                return data;
            }
            catch (Exception e)
            {
                throw new BusinessException($"Exception found: {e.Message}");
            }
        }

        public async Task<bool> updateSocioValidacion(Validacionupdate socioValidacion)
        {
            var socio = await _socios.getSocioById(socioValidacion.Id);
            if (socio == null)
            {
                _logger.LogError("El socio buscado no existe");
                throw new BusinessException("El socio buscado no existe");
            }

            var validacion = await _socios.getSocioValidacionbyValidacion(socio.Validacion);
            if (validacion == null)
            {
                _logger.LogError("El socio no cuenta con Id de validacion existente");
                throw new BusinessException("El socio no cuenta con Id de validacion existente");
            }

            if (socioValidacion.Autorizado == null)
            {
                _logger.LogError("El socio no cuenta con persona que autorice el proceso");
                throw new BusinessException("El socio no cuenta con persona que autorice el proceso");
            }

            var resultKyc = "";
            switch (socioValidacion.Estatus)
            {
                case 1:
                    resultKyc = "verified";
                    break;
                case 3:
                    resultKyc = "rejected";
                    break;
                case 2:
                    resultKyc = "reviewNeeded";
                    break;
            }


            var validation = new SocioValidacion()
            {
                Socio = validacion.Socio,
                Estatus = socioValidacion.Estatus,
                FechaValidacion = validacion.FechaValidacion,
                EstatusKyc = socioValidacion.Estatus,
                FechaKyc = DateTime.Now,
                FechaEmpresa = validacion.FechaEmpresa,
                ResultadoKyc = resultKyc,
                Observaciones = socioValidacion.Observaciones,
                ValidadoPor = validacion.ValidadoPor,
                AutorizadoPor = validacion.AutorizadoPor,
                Payload = new JsonParameter(JsonConvert.SerializeObject(validacion)),
                IdValidation = validacion.IdValidation,
                IdRelated = validacion.IdRelated,
                FechaInsert = DateTime.Now,
                FechaUpdate = DateTime.Now,
            };
            var validationResponse = await _socios.insertSocioValidacion(validation);
            if (validationResponse == 0)
            {
                throw new BusinessException("Lo sentimos hubo un problema al registrar esta nueva validacion");
            }
            var lastValidacion = await _socios.getLastValidacionById(validacion.Socio);
            if (lastValidacion == null)
            {
                _logger.LogError("No pudimos encontrar la ultima validacion del socio");
                throw new BusinessException("No pudimos encontrar la ultima validacion del socio");

            }
            var resUpdate = await _socios.updateSocioIdValidacion(lastValidacion.Validacion, lastValidacion.Socio);
            if (resUpdate == 0)
            {
                _logger.LogError("La Validacion del Socio no pudo ser actualizada");
                throw new BusinessException("La Validacion del Socio no pudo ser actualizada");
            }
            return true;
        }




        public async Task<IEnumerable<ResultSearchSocios>> getSocioSearch(SocioSearch socioSearch)
        {
            try
            {
                var data = await _socios.getSocioSearch(socioSearch);
                return data;
            }
            catch (Exception e)
            {
                throw new BusinessException($"Exception found: {e.Message}");
            }
        }

        public async Task<IEnumerable<ResultSearchSocioProduct>> getSocioProductSearch(SocioProductSearch socioProduct)
        {
            try
            {
                var data = await _socios.getSocioProductSearch(socioProduct);
                return data;
            }
            catch (Exception e)
            {
                throw new BusinessException($"Exception found: {e.Message}");
            }
        }


        public async Task<IEnumerable<SocioHistory>> getSocioHistory(int id)
        {
            try
            {
                var data = await _socios.getSocioHistory(id);
                return data;
            }
            catch (Exception e)
            {
                throw new BusinessException($"Exception found: {e.Message}");
            }
        }
        public async Task<SocioDetail> getSocioDetail(int id)
        {
            try
            {
                var data = await _socios.getSocioDetail(id);
                return data;
            }
            catch (Exception e)
            {
                throw new BusinessException($"Exception found: {e.Message}");
            }
        }

        public async Task<bool> updateStatusSocio(int id, int status)
        {
            var resUpdate = await _socios.updateStatusSocio(id, status);
            if (resUpdate == 0)
            {
                _logger.LogError("El estatus del Socio no pudo ser actualizado");
                throw new BusinessException("El estatus del Socio no pudo ser actualizado");
            }
            return true;
        }
    }
}
