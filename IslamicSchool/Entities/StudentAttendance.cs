namespace IslamicSchool.Entities
{
    public class StudentAttendance
    {
        public int Id { get; set; }
        public int AttendanceId { get; set; }
        public Attendance Attendance { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public bool IsPresent { get; set; }
    }
}
