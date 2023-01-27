using IslamicSchool.Entities;

namespace IslamicSchool.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
        void AddStudents(Student student);
        void UpdateStudents(Student student);
        void DeleteStudents(int id);
        Task<Student> FindStudent(int id);
        int CountStudents();
    }
}
