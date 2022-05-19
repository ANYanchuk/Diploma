using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TaskManager.Core.Models.Shared;

namespace TaskManager.ViewModels
{
    public class UserViewModel
    {
        public uint Id { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public List<ErrandViewModel>? Errands { get; set; }
    }
}