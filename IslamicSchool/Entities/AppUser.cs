
using Microsoft.AspNetCore.Identity;

namespace IslamicSchool.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public override string UserName { get; set; }
        public string? FatherName { get; set; }
        public int? BranchId { get; set; } 
        public Branch? Branch { get; set; }
        public ICollection<StudyClass> StudyClasses { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
   }
}
