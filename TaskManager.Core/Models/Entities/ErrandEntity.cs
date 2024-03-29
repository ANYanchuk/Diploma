#pragma warning disable CS8618
namespace TaskManager.Core.Models.Entities;

public class ErrandEntity
{
    public uint Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public string ReportFormat { get; set; }
    public string Type { get; set; }

    public List<UserEntity> Users { get; set; }
    public DateTime Started { get; set; }
    public DateTime? Deadline { get; set; }
    public string State { get; set; }
    public ReportEntity? Report { get; set; }
    public string? ReviewComment { get; set; }
}
