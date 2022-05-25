using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TaskManager.Core.Models.Shared;

namespace TaskManager.ViewModels
{
    public class PostErrandViewModel
    {
        public uint LeaderId { get; set; }
        public List<UserIdViewModel> Users { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string Type { get; set; }
        public Priority Priority { get; set; }
        public DateTime Started { get; private set; }
        public DateTime? Deadline { get; set; }
    }
}