using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Models;

namespace TaskManager.Data.DbContexts;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ApplicationRole> Roles { get; set; }
    public DbSet<Errand> Errands { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<UploadedFile> Files { get; set; }
    public DbSet<ReportFormat> ReportFormats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReportFormat>()
            .HasData(
                new ReportFormat("Файл"),
                new ReportFormat("Текст"));

        modelBuilder.Entity<ApplicationRole>()
            .HasData(
                new ApplicationRole("Завідувач"),
                new ApplicationRole("Викладач"),
                new ApplicationRole("Лаборант"));

        modelBuilder.Entity<Report>().HasMany(a => a.Files)
            .WithOne()
            .HasForeignKey(f => f.ReportId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<ApplicationUser>()
            .HasMany(u => u.Errands)
            .WithMany(e => e.Users)
            .UsingEntity(j => j.ToTable("UserErrands"));

        base.OnModelCreating(modelBuilder);
    }
}

