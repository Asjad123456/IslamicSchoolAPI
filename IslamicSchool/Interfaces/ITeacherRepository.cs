using IslamicSchool.Entities;

namespace IslamicSchool.Interfaces
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetTeachersAsync();
        void AddTeachers(Teacher teacher);
        void DeleteTeacher(int id);
        Task<Teacher> FindTeacher(int id);
    }
}
