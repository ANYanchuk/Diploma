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
    [Authorize]
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
        public IActionResult Get()
        {
            ServiceResponse<IEnumerable<UserEntity>> userResponse = usersService.GetAll();
            if (userResponse.IsSuccessfull)
                return Ok(mapper.Map<IEnumerable<UserViewModel>>(userResponse.Data));
            else
                return BadRequest(userResponse.ErrorMessage);
        }

        [HttpGet(UserIdURL)]
        public IActionResult GetById(uint userId)
        {
            ServiceResponse<UserEntity> userResponse = usersService.GetById(userId);
            if (userResponse.IsSuccessfull)
                return Ok(mapper.Map<UserViewModel>(userResponse.Data));
            else
                return BadRequest(userResponse.ErrorMessage);
        }

        [HttpPut(UserIdURL)]
        public IActionResult Put(uint userId, [FromBody] UserViewModel userViewModel)
        {
            UserEntity user = mapper.Map<UserEntity>(userViewModel);
            ServiceResponse<UserEntity> userResponse = usersService.Edit(user, userId);
            if (userResponse.IsSuccessfull)
                return Ok();
            else
                return BadRequest(userResponse.ErrorMessage);
        }

        [HttpGet(UserIdURL + "/errands")]
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
        public IActionResult GetErrand(uint userId, uint errandId)
        {
            ServiceResponse<ErrandEntity> taskResponse = tasksService.GetById(errandId);
            if (taskResponse.IsSuccessfull && taskResponse.Data!.Users.Select(u => u.Id).Contains(userId))
                return Ok(taskResponse.Data);
            else
                return NotFound();
        }

        [HttpPost("errands/{errandId}/report")]
        public IActionResult AddReport(uint errandId, [FromForm] ReportViewModel reportViewModel)
        {
            ReportEntity report = mapper.Map<ReportEntity>(reportViewModel);
            IEnumerable<byte[]> files = mapper.Map<IEnumerable<byte[]>>(reportViewModel.Files);

            IEnumerable<FileEntity>? fileEntities = reportViewModel?.Files?
                .Select(f => new FileEntity(f.FileName) { Content = mapper.Map<byte[]>(f) });

            ServiceResponse<ReportEntity> response = reportsService.Add(errandId, report, fileEntities);
            if (response.IsSuccessfull)
                return Created($"users/errands/{errandId}/report", response.Data);
            else
                return BadRequest(response.ErrorMessage);
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = "Завідувач")]
        public ActionResult DeleteUser(uint userId)
        {
            ServiceResponse<UserEntity> userResponse = usersService.Delete(userId);
            if (userResponse.IsSuccessfull)
                return NoContent();
            else
                return BadRequest(userResponse.ErrorMessage);
        }

        [HttpDelete( "errands/{errandId}/report")]
        public IActionResult DeleteReport(uint errandId)
        {
            ServiceResponse<ReportEntity> response = reportsService.Delete(errandId);
            if (response.IsSuccessfull)
                return NoContent();
            else return BadRequest(response.ErrorMessage);
        }

        [HttpGet("info")]
        [Authorize(Roles = "Завідувач")]
        public IActionResult Info()
        {
            ServiceResponse<IEnumerable<UserEntity>> response = usersService.GetAll();
            if (response.IsSuccessfull)
                return Ok(mapper.Map<IEnumerable<UserViewModel>>(response.Data));
            else return BadRequest(response.ErrorMessage);
        }
    }
}
