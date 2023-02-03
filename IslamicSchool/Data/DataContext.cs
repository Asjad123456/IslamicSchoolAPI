using IslamicSchool.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<BranchesXappUser> branchesXappUsers { get; set; } 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BranchesXappUser>()
                .HasKey(x => new { x.BranchId, x.AppUserId});

            builder.Entity<BranchesXappUser>()
                 .HasOne(x => x.AppUser)
                 .WithMany(x => x.branchesXappUsers)
                 .HasForeignKey(x => x.AppUserId);

            builder.Entity<BranchesXappUser>()
                .HasOne(x => x.Branch)
                .WithMany(x => x.branchesXappUsers)
                .HasForeignKey(x => x.BranchId);

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
            builder.Entity<Branch>()
                .HasMany(b => b.studyClasses)
                .WithOne(t => t.Branch)
                .HasForeignKey(t => t.BranchId);
            builder.Entity<Branch>()
                .HasMany(b => b.Students)
                .WithOne(s => s.Branch)
                .HasForeignKey(s => s.BranchId);
            builder.Entity<Branch>()
                .HasMany(b => b.AppUsers)
                .WithOne(b => b.Branch)
                .HasForeignKey(s => s.BranchId);
        }
    }
}
