using Entities.Entities;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Manager.Interfaces
{
    public interface IRecaudosManager
    {
        Task<bool> GetDataApi();
        Task<List<Recaudos>> GetRecaudos();
        Task<Report> GetReport(string datestring);
    }
}
