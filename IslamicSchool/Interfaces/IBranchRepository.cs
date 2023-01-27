using IslamicSchool.Entities;

namespace IslamicSchool.Interfaces
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetBranchesAsync();
        void AddBranch(Branch branch);
        void DeleteBranch(int id);
        Task<Branch> FindBranch(int id);
    }
}
