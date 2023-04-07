using IslamicSchool.Entities;

namespace IslamicSchool.DataTransferObjects.GetDataDtos
{
    public class GetClassDto
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public TimeOnly ClassTime { get; set; }
        public string ClassSubject { get; set; }
        public Guid TeacherId { get; set; }
        public string UserName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public int BranchId { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
