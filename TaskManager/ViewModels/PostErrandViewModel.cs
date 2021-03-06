using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TaskManager.Core.Models.Shared;

namespace TaskManager.ViewModels
{
    public class PostErrandViewModel
    {
        public List<UserIdViewModel> Users { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string Type { get; set; }
        public string? ReportFormat { get; set; }
        public DateTime? Deadline { get; set; }
    }
}