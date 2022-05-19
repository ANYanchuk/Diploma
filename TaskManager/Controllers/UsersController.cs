using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using TaskManager.ViewModels;
using TaskManager.Core.Services;
using TaskManager.Core.Models.Entities;
using TaskManager.Core.Models;
using TaskManager.Data.Helpers;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private const string UserIdURL = "{userId}";
        private readonly ITasksService tasksService;
        private readonly IMapper mapper;

        public UsersController(ITasksService tasksService, IMapper mapper)
        {
            this.tasksService = tasksService;
            this.mapper = mapper;
        }

        [HttpGet(UserIdURL + "/errands")]
        [Authorize]
        public ActionResult Get(uint userId)
        {
            ServiceResponse<IEnumerable<ErrandEntity>> taskResponse = 
                tasksService.GetAllForUser(userId);
            if (taskResponse.IsSuccessfull)
                return Ok(taskResponse.Data);
            else
                return BadRequest(taskResponse.ErrorMessage);
        }

        [HttpGet(UserIdURL + "/errands/{errandId}")]
        public IActionResult Get(uint userId, uint errandId)
        {
            ServiceResponse<ErrandEntity> taskResponse = tasksService.GetById(errandId);
            if (taskResponse.IsSuccessfull)
                return Ok(taskResponse.Data);
            else
                return NotFound();
        }
    }
}