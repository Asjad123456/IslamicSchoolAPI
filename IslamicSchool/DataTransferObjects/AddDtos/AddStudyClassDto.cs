namespace IslamicSchool.DataTransferObjects.AddDtos
{
    public class AddStudyClassDto
    {
        public string ClassName { get; set; }
        public TimeOnly ClassTime { get; set; }
        public Guid AppUserId { get; set; }
        public int BranchId { get; set; }
    }
}
