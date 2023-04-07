using IslamicSchool.Entities;

namespace IslamicSchool.Interfaces
{
    public interface IAdminTaskRepository
    {
        Task<IEnumerable<AdminTasks>> GetAdminTaksAsync();
        void AddAdminTasks(AdminTasks adminTasks);
        void DeleteAdminTasks(int id);
    }
}
