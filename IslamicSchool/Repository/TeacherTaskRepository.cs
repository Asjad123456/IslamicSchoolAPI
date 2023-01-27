using IslamicSchool.Data;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Repository
{
    public class TeacherTaskRepository : ITeacherTaskRepository
    {
        private readonly DataContext context;

        public TeacherTaskRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddTeacherTasks(TeacherTask teachertasks)
        {
            context.TeacherTasks.Add(teachertasks);
        }

        public void DeleteTeacherTasks(int id)
        {
            var teachertask = context.TeacherTasks.Find(id);
            context.TeacherTasks.Remove(teachertask);
        }

        public async Task<IEnumerable<TeacherTask>> GetTeachersTaksAsync()
        {
            return await context.TeacherTasks.ToListAsync();
        }
    }
}
