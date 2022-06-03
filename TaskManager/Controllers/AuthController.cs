using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using TaskManager.ViewModels;
using TaskManager.Core.Services;
using TaskManager.Core.Models.Entities;
using TaskManager.Core.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServise authService;
        private readonly IMapper mapper;

        public AuthController(IAuthServise authService, IMapper mapper)
        {
            this.authService = authService;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel model)
        {
            ServiceResponse<string> response = authService.Login(model.Email, model.Password);
            if (response.IsSuccessfull)
                return Ok(response.Data);
            else
                return BadRequest(response.ErrorMessage);

        }

        [HttpPost("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            UserEntity user = mapper.Map<UserEntity>(model);
            ServiceResponse<string> response = authService.Register(user, model.Password);
            if (response.IsSuccessfull)
                return Ok(response.Data);
            else
                return BadRequest(response.ErrorMessage);
        }

        [HttpPost("change-password")]
        public IActionResult ChangePassword(ChangePasswordViewModel passwordViewModel)
        {
            ServiceResponse<string> response = authService
                .ChangePassword(passwordViewModel.Email,
                                passwordViewModel.OldPassword,
                                passwordViewModel.NewPassword);
            if (response.IsSuccessfull)
                return Ok(response.Data);
            else
                return BadRequest(response.ErrorMessage);
        }

    }
}