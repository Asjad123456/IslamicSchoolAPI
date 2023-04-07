namespace IslamicSchool.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int BranchCode { get; set; }
        public int? SchoolId { get; set; }
        public School? School { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
        public ICollection<StudyClass> studyClasses { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
