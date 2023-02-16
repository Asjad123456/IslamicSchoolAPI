using IslamicSchool.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace IslamicSchool.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, Guid,
        IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {

        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudyClass> StudyClasses { get; set; }
        public DbSet<BranchAdmin> BranchAdmins { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<TeacherTask> TeacherTasks { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();
            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();
            builder.Entity<AppUser>()
                .HasMany(ur => ur.StudyClasses)
                .WithOne(u => u.AppUser)
                .HasForeignKey(u => u.AppUserId);
            builder.Entity<Branch>()
                .HasMany(b => b.AppUsers)
                .WithOne(t => t.Branch)
                .HasForeignKey(t => t.BranchId);
            builder.Entity<Branch>()
                .HasMany(b => b.studyClasses)
                .WithOne(t => t.Branch)
                .HasForeignKey(t => t.BranchId);
            builder.Entity<Branch>()
                .HasMany(b => b.Students)
                .WithOne(s => s.Branch)
                .HasForeignKey(s => s.BranchId);
            builder.Entity<StudyClass>()
                .HasMany(b => b.Students)
                .WithOne(s => s.StudyClass)
                .HasForeignKey(s => s.StudyClassId);
        }
    }
}
