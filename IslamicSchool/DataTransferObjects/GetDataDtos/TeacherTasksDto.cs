namespace IslamicSchool.DataTransferObjects.GetDataDtos
{
    public class TeacherTasksDto
    {
        public int id { get; set; }
        public string Task { get; set; }
        public Guid AppUserId { get; set; }
    }
}