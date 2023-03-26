namespace IslamicSchool.Entities
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public bool IsPresent { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
