namespace IslamicSchool.Entities
{
    public class BranchesXappUser
    {
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
