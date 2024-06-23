using LMS.Data.Consts;
using LMS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Admin> Admin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Teacher>().ToTable("Teacher");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Admin>().ToTable("Admin");

            SeedRoles(modelBuilder);

        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData
                (
                  new IdentityRole() { Id = "1", Name = ConstRoles.Admin  , NormalizedName = ConstRoles.Admin ,ConcurrencyStamp = ConstRoles.Admin     },
                  new IdentityRole() { Id = "2", Name = ConstRoles.Teacher, NormalizedName = ConstRoles.Teacher, ConcurrencyStamp = ConstRoles.Teacher },
                  new IdentityRole() { Id = "3", Name = ConstRoles.Student, NormalizedName = ConstRoles.Student, ConcurrencyStamp = ConstRoles.Student }
                );

        }
    }
}
