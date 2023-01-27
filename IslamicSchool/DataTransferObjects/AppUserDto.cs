using IslamicSchool.Entities;

namespace IslamicSchool.DataTransferObjects
{
    public class AppUserDto
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string? FatherName { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
