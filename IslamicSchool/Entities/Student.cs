namespace IslamicSchool.Entities
{
    public class Student
    {
        public int id { get; set; }
        public int? RegNumber { get; set; }
        public string Name { get; set; }
        public string? FatherName { get; set; }
        public int? ContactNumber { get; set; }
        public string? Address { get; set; }
        public int RollNumber { get; set; }
        public int? GuardianId { get; set;  }
        public Guardian? Guardian { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
