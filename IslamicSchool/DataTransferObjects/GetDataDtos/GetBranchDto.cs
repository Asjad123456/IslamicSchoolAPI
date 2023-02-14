using IslamicSchool.Entities;

namespace IslamicSchool.DataTransferObjects.GetDataDtos
{
    public class GetBranchDto
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int BranchCode { get; set; }
        public Guid BranchAdminId { get; set; }
        public string UserName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<StudyClass> StudyClasses { get; set; }

    }
}
