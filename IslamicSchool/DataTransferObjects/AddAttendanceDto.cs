namespace IslamicSchool.DataTransferObjects
{
    public class AddAttendanceDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public int StudentId { get; set; }
        public int StudyClassId { get; set; }
    }
}
