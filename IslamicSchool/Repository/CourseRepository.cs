using IslamicSchool.Data;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext context;

        public CourseRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddCourse(Course course)
        {
            context.Courses.Add(course);
        }

        public void DeleteCourse(int id)
        {
            var course = context.Courses.Find(id);
            context.Courses.Remove(course);
        }

        public async Task<Course> FindCourse(int id)
        {
            return await context.Courses.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await context.Courses.ToListAsync();
        }
    }
}
