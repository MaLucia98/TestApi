using Entities.Entities;
using Logic.Manager.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.Data.MySqlClient;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAngularOrigins")]
    public class RegistrationsController : ControllerBase
    {  
        private readonly ILogger<RegistrationsController> _logger;
        private readonly IRegistrationsManager registrationsManager;

        public RegistrationsController(ILogger<RegistrationsController> logger, IRegistrationsManager registrationsManager)
        {
            _logger = logger;
            this.registrationsManager = registrationsManager;
        }




        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var partner = await this.registrationsManager.Read();
            return Ok(partner);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var partner = await this.registrationsManager.ReadById(id);
            return Ok(partner);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PartnerSubject model)
        {
            try
            {
                var createdSubject = await this.registrationsManager.Create(model);
                return Ok(createdSubject);

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PartnerSubject model)
        {
            try
            {
                var createdSubject = await this.registrationsManager.Update(id, model);
                return Ok(createdSubject);

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDelete = await this.registrationsManager.Delete(id);
            if (isDelete)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("Subjects")]
        public async Task<IActionResult> GetSubjects()
        {
            var partner = await this.registrationsManager.GetSubjects();
            return Ok(partner);
        }

    }
}