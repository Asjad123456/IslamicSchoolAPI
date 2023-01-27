using IslamicSchool.Entities;

namespace IslamicSchool.Interfaces
{
    public interface IBranchAdminRepository
    {
        Task<IEnumerable<BranchAdmin>> GetBranchAdminsAsync();
        void AddBranchAdmin(BranchAdmin branchAdmin);
        void DeleteBranchAdmin(int id);
        Task<BranchAdmin> FindBranchAdmin(int id);
    }
}
