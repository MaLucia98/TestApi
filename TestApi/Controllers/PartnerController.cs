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
    public class PartnerController : ControllerBase
    {  
        private readonly ILogger<RegistrationsController> _logger;
        private readonly IPartnerManager partnerManager;

        public PartnerController(ILogger<RegistrationsController> logger, IPartnerManager partnerManager)
        {
            _logger = logger;
            this.partnerManager = partnerManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var partner = await this.partnerManager.Read();
            return Ok(partner);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var partner = await this.partnerManager.ReadById(id);
            return Ok(partner);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Partner model)
        {
            try
            {
                var createdSubject = await this.partnerManager.Create(model);
                return Ok(createdSubject);

            }
            catch (InvalidOperationException ex)
            {              
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Partner model)
        {
            try
            {
                var createdSubject = await this.partnerManager.Update(id, model);
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
            var isDelete = await this.partnerManager.Delete(id);
            if (isDelete)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var partner = await this.partnerManager.GetStudets();
            return Ok(partner);
        }

        [HttpGet("{id}/bySubject")]
        public async Task<IActionResult> GetStudentsbySubject(int id)
        {
            var partner = await this.partnerManager.GetStudetsBySubject(id);
            return Ok(partner);
        }

        [HttpGet("{id}/byPartner")]
        public async Task<IActionResult> GetSubjectsByPartner(int id)
        {
            var partner = await this.partnerManager.GetSubjectsByPartner(id);
            return Ok(partner);
        }
    }
}