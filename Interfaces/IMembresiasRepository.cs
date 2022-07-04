﻿using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IMembresiasRepository
    {
        Task<IEnumerable<Membresias>> GetMembresias(MembresiasFilter filter);
        Task<bool> processNewMembership(NewMembership membresia);
    }
}