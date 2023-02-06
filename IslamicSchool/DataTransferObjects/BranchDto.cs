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
        /*        public Array BranchAdmin { get; set; }*/
        public string UserName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }

    }
}