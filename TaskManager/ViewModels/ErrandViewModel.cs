namespace TaskManager.ViewModels;
public class ErrandViewModel
{
    public ErrandViewModel(string title, string reportFormatName, string type, DateTime started)
    {
        this.Title = title;
        this.ReportFormatName = reportFormatName;
        this.Type = type;
        this.Started = started;

    }

    public uint? Id { get; set; }
    public List<UserViewModel>? Users { get; set; }
    public string Title { get; set; }
    public string? Body { get; set; }
    public string ReportFormatName { get; set; }
    public string Type { get; set; }
    public DateTime Started { get; set; }
    public ReportViewModel? Report { get; set; }
    public DateTime? Deadline { get; set; }
}
