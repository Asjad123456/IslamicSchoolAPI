namespace IslamicSchool.Entities
{
    public class StudyClass
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public DateTime ClassTime { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
/*        public int CourseId { get; set; }
        public Course Course { get; set; }*/
    }
}
