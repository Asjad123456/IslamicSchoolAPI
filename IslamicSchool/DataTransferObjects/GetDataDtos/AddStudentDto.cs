using IslamicSchool.Entities;
using System.ComponentModel.DataAnnotations;

namespace IslamicSchool.DataTransferObjects.GetDataDtos
{
    public class AddStudentDto
    {
        public int RegNumber { get; set; }
        [Required]
        public string Name { get; set; }
        public string? FatherName { get; set; }
        public int ContactNumber { get; set; }
        public string? Address { get; set; }
        [Required]
        public int RollNumber { get; set; }
        public int BranchId { get; set; }
        public int? GuardianId { get; set; }
        public int StudyClassId { get; set; }
    }
}
