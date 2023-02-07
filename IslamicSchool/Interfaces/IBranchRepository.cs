using IslamicSchool.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IslamicSchool.Interfaces
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetBranchesAsync();
        Task<IActionResult> GetBranchByIdAsync(int id);
        void AddBranch(Branch branch);
        void DeleteBranch(int id);
        Task<Branch> FindBranch(int id);
    }
}
