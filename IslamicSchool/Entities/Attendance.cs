    namespace IslamicSchool.Entities
    {
        public class Attendance
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public bool IsPresent { get; set; }
            public int StudentId { get; set; }
            public Student? Student { get; set; }
            public int StudyClassId { get; set; }
            public StudyClass? StudyClass { get; set; }
        }
    }
