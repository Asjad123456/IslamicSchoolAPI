using IslamicSchool.Data;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Repository
{
    public class BranchAdminRepository : IBranchAdminRepository
    {
        private readonly DataContext context;

        public BranchAdminRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddBranchAdmin(BranchAdmin branchAdmin)
        {
            context.BranchAdmins.Add(branchAdmin);
        }

        public void DeleteBranchAdmin(int id)
        {
            var admin = context.BranchAdmins.Find(id);
            context.BranchAdmins.Remove(admin);
        }

        public async Task<BranchAdmin> FindBranchAdmin(int id)
        {
            return await context.BranchAdmins.FindAsync(id);
        }

        public async Task<IEnumerable<BranchAdmin>> GetBranchAdminsAsync()
        {
            return await context.BranchAdmins.ToListAsync();
        }
    }
}
