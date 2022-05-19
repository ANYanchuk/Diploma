using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Core.Services;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Entities;
using TaskManager.Core.Models.Data;
using TaskManager.Data.DbContexts;
using TaskManager.Data.Helpers;

using static TaskManager.Data.Helpers.TokenHelper;

namespace TaskManager.Data.Services
{
    public class AuthServise : IAuthServise
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public AuthServise(IConfiguration configuration, ApplicationDbContext context, IMapper mapper)
        {
            this.configuration = configuration;
            this.context = context;
            this.mapper = mapper;
        }

        public ServiceResponse<string> Register(UserEntity user, string password)
        {
            if (password is null)
                return new(false, message: "NULLOV");
            if(context.Users.Any(u => u.Email == user.Email))
                return new(false, message: "Email already exists");

            string hashedPassword = PasswordHasher.HashPassword(password);
            ApplicationUser appUser = mapper.Map<ApplicationUser>(user);
            appUser.PasswordHash = hashedPassword;
            context.Users.Add(appUser);
            context.SaveChanges();
            ApplicationUser newUser = context.Users.First(u => u.Email == appUser.Email);
            string token = GetToken(newUser, configuration);
            return new(true, data: token);
        }

        public ServiceResponse<string> Login(string email, string password)
        {
            ApplicationUser? user = context.Users.FirstOrDefault(u => u.Email == email);
            if (user is null)
                return new(false, message: "Email incorrect");
            if (!PasswordHasher.VerifyPassword(password, user.PasswordHash))
                return new(false, message: "Incorrect passsword");

            string token = GetToken(user, configuration);

            return new(true, data: token);
        }
    }
}