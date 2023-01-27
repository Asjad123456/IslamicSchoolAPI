using IslamicSchool.Entities;

namespace IslamicSchool.Interfaces
{
    public interface ITeacherTaskRepository
    {
        Task<IEnumerable<TeacherTask>> GetTeachersTaksAsync();
        void AddTeacherTasks(TeacherTask teachertasks);
        void DeleteTeacherTasks(int id);
    }
}
