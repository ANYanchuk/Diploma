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
    public class RolesController : ControllerBase
    {
        private const string UserIdURL = "{userId}";
        private readonly IRolesService rolesService;
        private readonly IMapper mapper;

        public RolesController(IRolesService rolesService, IMapper mapper)
        {
            this.rolesService = rolesService;
            this.mapper = mapper;
        }

        [HttpGet()]
        [Authorize(Roles = "Завідувач")]
        public ActionResult Get() => Ok(rolesService.GetAll().Data);

        [HttpPost("{role}")]
        [Authorize(Roles = "Завідувач")]
        public ActionResult Post(string role)
        {
            ServiceResponse<string> response = rolesService.Add(role);
            if (response.IsSuccessfull)
                return Created("/api/roles/", response.Data);
            else
                return BadRequest(response.Data);
        }

        [HttpPost("{role}")]
        [Authorize(Roles = "Завідувач")]
        public ActionResult Delete(string role)
        {
            if(role == "Завідувач")
                return Forbid();
            
            ServiceResponse<string> response = rolesService.Delete(role);
            if (response.IsSuccessfull)
                return NoContent();
            else
                return NotFound();
        }
    }
}
