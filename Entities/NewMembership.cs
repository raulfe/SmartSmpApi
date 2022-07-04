using SmartBusinessAPI.Models;
using System.Collections.Generic;

namespace SmartBusinessAPI.Entities
{
    public class NewMembership
    {
        public Membresias Membresia { get; set; }
        public List<MembresiaPais> Paises { get; set; }
    }
}
