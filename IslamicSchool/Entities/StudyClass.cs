using System.ComponentModel.DataAnnotations.Schema;

namespace IslamicSchool.Entities
{
    public class StudyClass
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        [NotMapped]
        public TimeOnly ClassTime { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
