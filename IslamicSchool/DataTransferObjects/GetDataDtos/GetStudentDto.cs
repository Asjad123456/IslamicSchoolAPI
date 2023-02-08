namespace IslamicSchool.DataTransferObjects.GetDataDtos
{
    public class GetStudentDto
    {
        public int id { get; set; }
        public int RegNumber { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public int ContactNumber { get; set; }
        public string Address { get; set; }
        public int RollNumber { get; set; }
        public int GuardianId { get; set; }
        public string GuardianName { get; set; }
        public string GuardianFatherName { get; set; }
        public int GuardianContactNumber { get; set; }
        public string GuardianAddress { get; set; }
        public int CNIC { get; set; }
    }
}
