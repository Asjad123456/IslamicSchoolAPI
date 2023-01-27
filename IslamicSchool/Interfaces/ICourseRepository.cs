using IslamicSchool.Entities;

namespace IslamicSchool.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCoursesAsync();
        void AddCourse(Course course);
        void DeleteCourse(int id);
        Task<Course> FindCourse(int id);
    }
}
