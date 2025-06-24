using Microsoft.EntityFrameworkCore;
using FessorApi.Models;

namespace FessorApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Demo> Demos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<School>()
                .HasMany(s => s.Students)
                .WithOne(s => s.School)
                .HasForeignKey(s => s.SchoolId);

            modelBuilder.Entity<School>()
                .HasMany(s => s.Users)
                .WithOne(u => u.School)
                .HasForeignKey(u => u.SchoolId)
                .IsRequired(false);

            modelBuilder.Entity<School>()
                .HasMany(s => s.Reports)
                .WithOne(r => r.School)
                .HasForeignKey(r => r.SchoolId);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Reports)
                .WithOne(r => r.Student)
                .HasForeignKey(r => r.StudentId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reports)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);
        }
    }
} 