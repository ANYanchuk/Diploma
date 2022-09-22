using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Models;
using TaskManager.Core.Services;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportFormatsController : ControllerBase
    {
        private const string UserIdURL = "{userId}";
        private readonly IReportFormatsService reportFormatsService;
        private readonly IMapper mapper;

        public ReportFormatsController(IReportFormatsService reportFormatsService, IMapper mapper)
        {
            this.reportFormatsService = reportFormatsService;
            this.mapper = mapper;
        }

        [HttpGet()]
        [Authorize(Roles = "Завідувач")]
        public ActionResult Get() => Ok(reportFormatsService.GetAll().Data);

        [HttpPost()]
        [Authorize(Roles = "Завідувач")]
        public ActionResult Post([FromBody] string format)
        {
            ServiceResponse<string> response = reportFormatsService.Add(format);
            if (response.IsSuccessfull)
                return Created("/api/roles/", response.Data);
            else
                return BadRequest(response.Data);
        }

        [HttpDelete("{format}")]
        [Authorize(Roles = "Завідувач")]
        public ActionResult Delete(string format)
        {   
            ServiceResponse<string> response = reportFormatsService.Delete(format);
            if (response.IsSuccessfull)
                return NoContent();
            else
                return NotFound();
        }
    }
}
