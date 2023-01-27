using IslamicSchool.Entities;

namespace IslamicSchool.Interfaces
{
    public interface IGuardianRepository
    {
        Task<IEnumerable<Guardian>> GetGuardianAsync();
        void AddGuardian(Guardian guardian);
        void DeleteGuardian(int id);
        Task<Guardian> FindGuardian(int id);
    }
}
