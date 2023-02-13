using IslamicSchool.Entities;

namespace IslamicSchool.DataTransferObjects
{
    public class AppUserDto
    {
        public Guid id { get; set; }
        public string UserName { get; set; }
        public string? FatherName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
    }
}
