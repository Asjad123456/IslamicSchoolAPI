using IslamicSchool.Entities;

namespace IslamicSchool.Interfaces
{
    public interface ISchoolRepository
    {
        Task<IEnumerable<School>> GetSchoolsAsync();
        void AddSchool(School school);
        void UpdateSchool(School school);
        void DeleteSchool(int id);
        Task<School> FindSchool(int id);
    }
}
