using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IMetamapRepository
    {
        Task<bool> syncVerification(string url);
    }
}
