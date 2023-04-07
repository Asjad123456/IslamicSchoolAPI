namespace IslamicSchool.Entities
{
    public class School
    {
        public int id { get; set; }
        public string Name { get; set; }
        public Guid Code { get; set; }
        public ICollection<Branch> Branches { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection <StudyClass> StudyClasses { get; set; }
    }
}
