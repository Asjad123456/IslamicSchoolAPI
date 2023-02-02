namespace IslamicSchool.DataTransferObjects
{
    public class BranchDto
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string City { get; set;}
        public string Address { get; set; }
        public int BranchCode { get; set; }
        public Guid BranchAdminId { get; set; }
    }
}