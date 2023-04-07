using IslamicSchool.Data;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Repository
{
    public class AdminTaskRespository : IAdminTaskRepository
    {
        private readonly DataContext context;

        public AdminTaskRespository(DataContext context)
        {
            this.context = context;
        }

        public void AddAdminTasks(AdminTasks adminTasks)
        {
            context.AdminTasks.Add(adminTasks);
        }

        public void DeleteAdminTasks(int id)
        {
            var adminTask = context.AdminTasks.Find(id);
            context.AdminTasks.Remove(adminTask);
        }

        public async Task<IEnumerable<AdminTasks>> GetAdminTaksAsync()
        {
            return await context.AdminTasks.ToListAsync();
        }
    }
}
