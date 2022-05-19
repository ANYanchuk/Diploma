using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using TaskManager.Core.Models.Data;

namespace TaskManager.Data.DbContexts;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ApplicationRole> Roles { get; set; }
    public DbSet<Errand> Errands { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<UploadedFile> Files { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ReportFormat> ReportFormats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasData(
                new Category("Лекція"),
                new Category("Інше"));

        modelBuilder.Entity<ReportFormat>()
            .HasData(
                new ReportFormat("Файл"),
                new ReportFormat("Email"));

        modelBuilder.Entity<ApplicationRole>()
            .HasData(
                new ApplicationRole("Завідувач"),
                new ApplicationRole("Викладач"),
                new ApplicationRole("Лаборант"));

        modelBuilder.Entity<Report>().HasMany(a => a.Files)
            .WithOne()
            .HasForeignKey(f => f.AnswerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}

