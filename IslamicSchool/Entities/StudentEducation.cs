using System.Security.Permissions;

namespace IslamicSchool.Entities
{
    public class StudentEducation
    {
        public int Id { get; set; }
        public string CurrentStudyLevel { get; set; }
        public int MarksInMatric { get; set; }
        public int MarksInIntermedicate { get; set; }
        public string Remarks { get; set; }

    }
}
