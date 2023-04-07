namespace IslamicSchool.Entities
{
    public class AdminTasks
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
