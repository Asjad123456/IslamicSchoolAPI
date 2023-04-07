namespace IslamicSchool.DataTransferObjects
{
    public class StudentDto
    {
        public int id { get; set; }
        public int? RegNumber { get; set; }
        public string Name { get; set; }
        public string? FatherName { get; set; }
        public int? ContactNumber { get; set; }
        public string? Address { get; set; }
        public int? RollNumber { get; set; }
        public string? Guardian { get; set; }
        public string? StudentEducation { get; set; }
    }
}
