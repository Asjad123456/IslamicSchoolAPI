namespace IslamicSchool.DataTransferObjects
{
    public class StudyClassDto
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public DateTime ClassTime { get; set; }
        public Guid TeacherId { get; set; }
        public int BranchId { get; set; }
    }
}
