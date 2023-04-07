using IslamicSchool.Entities;

namespace IslamicSchool.Interfaces
{
    public interface IStudentEducationRepository
    {
        Task<IEnumerable<StudentEducation>> GeEducationAsync();
        void AddEducation(StudentEducation studentEducation);
        void UpdateEducation(StudentEducation studentEducation);
        void DeleteEducation(int id);
    }
}
