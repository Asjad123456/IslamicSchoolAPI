namespace IslamicSchool.DataTransferObjects
{
    public class AttendanceRecordDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int RollNumber { get; set; }
        public bool IsPresent { get; set; }
    }
}
