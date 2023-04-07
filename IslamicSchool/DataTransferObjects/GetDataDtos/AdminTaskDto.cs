namespace IslamicSchool.DataTransferObjects.GetDataDtos
{
    public class AdminTaskDto
    {
        public int id { get; set; }
        public string Task { get; set; }
        public Guid AppUserId { get; set; }
    }
}
