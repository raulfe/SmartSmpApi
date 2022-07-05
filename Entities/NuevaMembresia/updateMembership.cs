using SmartBusinessAPI.Models;
using System.Collections.Generic;

namespace SmartBusinessAPI.Entities.NuevaMembresia
{
    public class updateMembership
    {

        public Membresias membresia { get; set; }

        public List<MembresiaPais> Paises { get; set; }

    }
}
