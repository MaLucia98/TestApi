using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Manager.Interfaces
{
    public interface IDataApiManager
    {
        Task<List<RecaudoVehiculos>> GetRecaudoVehiculosApi(string date);
        Task<List<ConteoVehiculos>> GetConteoVehiculosApi(string date);
    }
}
