using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.DbContexts;
using TaskManager.Core.Models.Data;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private ApplicationDbContext context;
        public const string BasePrefix = "api/files/";
        public FilesController(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        [HttpGet("{id}")]
        public IActionResult GetFile([FromRoute] int id)
        {
            UploadedFile? file = context.Files.FirstOrDefault(f => f.Id == id);
            if (file is null)
                return NotFound();

            try
            {
                FileStream fs = new FileStream(file.Path, FileMode.Open);
                return File(fs, "multipart/form-data", file.Name);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
