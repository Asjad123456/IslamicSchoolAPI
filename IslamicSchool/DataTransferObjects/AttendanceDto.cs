namespace IslamicSchool.DataTransferObjects
{
    public class AttendanceDto
    {
        public DateTime Date { get; set; }
        public int StudyClassId { get; set; }
        public List<AttendanceRecordDto> AttendanceRecords { get; set; }
    }
}
