using IslamicSchool.Data;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DataContext context;

        public TeacherRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddTeachers(Teacher teacher)
        {
            context.Teachers.Add(teacher);        
        }

        public void DeleteTeacher(int id)
        {
            var teacher = context.Teachers.Find(id);
            context.Teachers.Remove(teacher);
        }

        public async Task<Teacher> FindTeacher(int id)
        {
            return await context.Teachers.FindAsync(id);
        }

        public async Task<IEnumerable<Teacher>> GetTeachersAsync()
        {
            return await context.Teachers.ToListAsync();
        }
    }
}
