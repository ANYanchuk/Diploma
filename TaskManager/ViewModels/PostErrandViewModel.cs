using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TaskManager.Core.Models.Shared;

namespace TaskManager.ViewModels
{
    public class PostErrandViewModel
    {
        public PostErrandViewModel(
            string title,
            string type,
            string reportFormat,
            DateTime deadline,
            IEnumerable<UserIdViewModel> users)
        {
            this.Title = title;
            this.Type = type;
            this.ReportFormat = reportFormat;
            this.Deadline = deadline;
            this.Users = users.ToList();
        }
        public string Title { get; set; }
        public string? Body { get; set; }
        public string Type { get; set; }
        public string ReportFormat { get; set; }
        public DateTime Deadline { get; set; }
        public List<UserIdViewModel> Users { get; set; }
    }
}