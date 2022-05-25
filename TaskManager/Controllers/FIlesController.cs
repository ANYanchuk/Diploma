using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.DbContexts;

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
        public async Task<IActionResult> GetFile([FromRoute] int id)
        {
            var file = await context.Files.FindAsync(id);
            if (file is null)
                return NotFound();

            FileStream fs = new FileStream(file.Path, FileMode.Open);
            return File(fs, "multipart/form-data", file.Name);
        }
    }
}
