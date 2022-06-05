using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TaskManager.Core.Models.Shared;

namespace TaskManager.ViewModels
{
    public class ErrandViewModel
    {
        public uint Id { get; set; }
        public List<UserViewModel> Users { get; set; }
        public string Title { get; set; }
        public string? Body { get; set; }
        public string ReportFormatName { get; set; }
        public string Type { get; set; }
        public DateTime Started { get; set; }
        public ReportViewModel? Report { get; set; }
        public DateTime? Deadline { get; set; }
    }
}