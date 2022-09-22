using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Entities;
using TaskManager.Core.Services;
using TaskManager.ViewModels;

namespace TaskManager.Controllers
{
    [ApiController]
    [Authorize(Roles = "Завідувач")]
    [Route("api/[controller]")]
    public class ErrandsController : ControllerBase
    {
        private readonly ITasksService tasksService;
        private readonly IMapper mapper;

        public ErrandsController(ITasksService taskService, IMapper mapper)
        {
            this.tasksService = taskService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult Get()
        {
            ServiceResponse<IEnumerable<ErrandEntity>> taskResponse = tasksService.GetAll();
            if (taskResponse.IsSuccessfull)
                return Ok(taskResponse.Data);
            else
                return BadRequest(taskResponse.ErrorMessage);
        }

        [HttpGet("{id}")]
        public IActionResult Get(uint id)
        {
            ServiceResponse<ErrandEntity> taskResponse = tasksService.GetById(id);
            if (taskResponse.IsSuccessfull)
                return Ok(taskResponse.Data);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Post(PostErrandViewModel errandViewModel)
        {
            ErrandEntity task = mapper.Map<ErrandEntity>(errandViewModel);
            ServiceResponse<ErrandEntity> response = tasksService.Add(task);
            if (response.IsSuccessfull)
                return Created($"/api/tasks/{response.Data?.Id}", response.Data);
            else
                return BadRequest(response.ErrorMessage);
        }

        [HttpPut("{id}")]
        public IActionResult Put(PostErrandViewModel errandViewModel, [FromRoute] uint id)
        {
            ErrandEntity task = mapper.Map<ErrandEntity>(errandViewModel);
            ServiceResponse<ErrandEntity> response = tasksService.Edit(task, id);
            if (response.IsSuccessfull)
                return Ok(response.Data);
            else
                return BadRequest(response.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] uint id)
        {
            ServiceResponse<string> response = tasksService.Delete(id);
            if (response.IsSuccessfull)
                return NoContent();
            else
                return BadRequest(response.ErrorMessage);
        }

        [HttpGet("info")]
        public IActionResult Info()
        {
            ServiceResponse<IEnumerable<ErrandInfo>> response = tasksService.GetInfo();
            if (response.IsSuccessfull)
                return Ok(response.Data);
            else
                return BadRequest(response.ErrorMessage);
        }
    }
}
