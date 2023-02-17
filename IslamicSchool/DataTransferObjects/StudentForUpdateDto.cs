namespace IslamicSchool.DataTransferObjects
{
    public class StudentForUpdateDto
    {
        public string Name { get; set; }
        public string FatherName { get; set; }
        public int ContactNumber { get; set; }
        public string Address { get; set; }
        public int RollNumber { get; set; }
        public int? GuardianId { get; set; }
        public string? GuardianName { get; set; }
        public int GuardianContactNumber { get; set; }
        public string? GuardianAddress { get; set; }
        public string? GuardianFatherName { get; set; }
    }
}
