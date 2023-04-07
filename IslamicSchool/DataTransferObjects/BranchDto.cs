namespace IslamicSchool.DataTransferObjects
{
    public class BranchDto
    {
        public string BranchName { get; set; }
        public string City { get; set;}
        public string Address { get; set; }
        public int BranchCode { get; set; }
        public Guid AppUserId { get; set; }
        public int SchoolId { get; set; }
    }
}