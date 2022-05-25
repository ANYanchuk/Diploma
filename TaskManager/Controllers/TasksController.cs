using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using TaskManager.ViewModels;
using TaskManager.Core.Services;
using TaskManager.Core.Models.Entities;
using TaskManager.Core.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService tasksService;
        private readonly IMapper mapper;

        public TasksController(ITasksService taskService, IMapper mapper)
        {
            this.tasksService = taskService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Завідувач")]
        [HttpGet]
        public ActionResult Get()
        {
            ServiceResponse<IEnumerable<ErrandEntity>> taskResponse = tasksService.GetAll();
            if (taskResponse.IsSuccessfull)
                return Ok(taskResponse.Data);
            else
                return BadRequest(taskResponse.ErrorMessage);
        }

        [Authorize(Roles = "Завідувач")]
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

        [Authorize(Roles = "Завідувач")]
        [HttpPut("{id}")]
        public IActionResult Put(PostErrandViewModel errandViewModel, [FromRoute] uint id)
        {
            ErrandEntity task = mapper.Map<ErrandEntity>(errandViewModel);
            ServiceResponse<ErrandEntity> response = tasksService.Edit(task, id);
            if (response.IsSuccessfull)
                return Ok(null);
            else
                return BadRequest(response.ErrorMessage);
        }

        [Authorize(Roles = "Завідувач")]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] uint id)
        {
            ServiceResponse<string> response = tasksService.Delete(id);
            if (response.IsSuccessfull)
                return Ok(null);
            else
                return BadRequest(response.ErrorMessage);
        }
    }
}