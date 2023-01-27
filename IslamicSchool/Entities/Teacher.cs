namespace IslamicSchool.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string TeacherName { get; set; }
        public string FatherName { get; set; }
        public int CNIC { get; set; }
        public int ContactNumber { get; set; }
        public string Address { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
