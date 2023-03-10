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
        public ICollection<AppUser> AppUsers { get; set; }
        public ICollection<StudyClass> StudyClasses { get; set; }
        public ICollection<Student> Students { get; set; }

    }
}
