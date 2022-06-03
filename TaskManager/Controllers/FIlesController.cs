using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.DbContexts;
using TaskManager.Core.Services;
using TaskManager.Data.Services;
using TaskManager.Core.Models.Entities;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService fileService;
        private IMapper mapper;
        public const string BasePrefix = "api/files/";
        public FilesController(IFileService fileService, IMapper mapper)
        {
            this.fileService = fileService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetFile([FromRoute] int id)
        {
            var result = fileService.GetFile(id);
            if (result is null)
                return NotFound();

            FileStream fs = new FileStream(result.Value.Path, FileMode.Open);
            return File(fs, "application/file", result.Value.Name);
        }

        [HttpGet("errand-info")]
        public IActionResult ErrandVedomost([FromQuery] DateTime since, [FromQuery] DateTime till)
        {
            Stream file = fileService.GetErrandsDoc(since, till);
            return File(file, "application/msword", "Sample.docx");
        }

        [HttpGet("user-info")]
        public IActionResult UserVedomost(DateTime since, DateTime till, uint userId)
        {
            Stream? file = fileService.GetUsersDoc(since, till, userId);
            if (file is null)
                return NotFound();
            return File(file, "application/msword", "Sample.docx");
        }
    }
}
