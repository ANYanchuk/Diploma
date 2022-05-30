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
        private readonly IReportsService reportsService;
        private readonly IMapper mapper;
        private readonly IUsersService usersService;

        public UsersController(
            IMapper mapper,
            ITasksService tasksService,
            IReportsService reportsService,
            IUsersService usersService)
        {
            this.usersService = usersService;
            this.reportsService = reportsService;
            this.tasksService = tasksService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(uint userId)
        {
            ServiceResponse<IEnumerable<UserEntity>> userResponse = usersService.GetAll();

            if (userResponse.IsSuccessfull)
                return Ok(mapper.Map<UserViewModel>(userResponse.Data));
            else
                return BadRequest(userResponse.ErrorMessage);
        }

        [HttpGet(UserIdURL + "/errands")]
        [Authorize]
        public IActionResult GetErrands(uint userId)
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
            if (taskResponse.IsSuccessfull && taskResponse.Data.Users.Select(u => u.Id).Contains(userId))
                return Ok(taskResponse.Data);
            else
                return NotFound();
        }

        [HttpPost(UserIdURL + "/errands/{errandId}/report")]
        public IActionResult AddReport(uint userId, uint errandId, [FromForm] ReportViewModel reportViewModel)
        {
            ReportEntity report = mapper.Map<ReportEntity>(reportViewModel);
            IEnumerable<byte[]> files = mapper.Map<IEnumerable<byte[]>>(reportViewModel.Files);

            IEnumerable<FileEntity>? fileEntities = reportViewModel?.Files?
                .Select(f => new FileEntity(f.FileName) { Content = mapper.Map<byte[]>(f) });

            ServiceResponse<ReportEntity> response = reportsService.Add(errandId, report, fileEntities);
            if (response.IsSuccessfull)
                return Created($"users/{userId}/errands/{errandId}/report", response.Data);
            else
                return BadRequest(response.ErrorMessage);
        }

        [HttpDelete]
        [Authorize(Roles = "Завідувач")]
        public ActionResult DeleteUser(uint userId)
        {
            ServiceResponse<UserEntity> userResponse = usersService.Delete(userId);
            if (userResponse.IsSuccessfull)
                return NoContent();
            else
                return BadRequest(userResponse.ErrorMessage);
        }

        [HttpDelete(UserIdURL + "/errands/{errandId}/report")]
        public IActionResult DeleteReport(uint userId, uint errandId)
        {
            ServiceResponse<ReportEntity> response = reportsService.Delete(errandId);
            if (response.IsSuccessfull)
                return NoContent();
            else
                return BadRequest(response.ErrorMessage);
        }
    }
}