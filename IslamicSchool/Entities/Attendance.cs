namespace IslamicSchool.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int StudyClassId { get; set; }
        public StudyClass StudyClass { get; set; }
        public ICollection<AttendanceRecord> AttendanceRecords { get; set; }
    }
}
