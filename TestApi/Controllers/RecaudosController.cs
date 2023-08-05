using Entities.Entities;
using Logic.Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecaudosController : ControllerBase
    {  
        private readonly ILogger<RecaudosController> _logger;
        private readonly IRecaudosManager recaudosManager;

        public RecaudosController(ILogger<RecaudosController> logger, IRecaudosManager recaudosManager)
        {
            _logger = logger;
            this.recaudosManager = recaudosManager;
        }

        [HttpGet("DataApi")]
        public Task<bool> GetDataApi()
        {
            return this.recaudosManager.GetDataApi();
        }

        [HttpGet("Recaudos")]
        public Task<List<Recaudos>> GetRecaudos()
        {
            return this.recaudosManager.GetRecaudos();
        }

        [HttpGet("Reporte")]
        public Task<Report> GetReporte(string date)
        {
            return this.recaudosManager.GetReport(date);
        }
    }
}