using IslamicSchool.Data;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly DataContext context;

        public BranchRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddBranch(Branch branch)
        {
            context.Branches.Add(branch);
        }
        public void DeleteBranch(int id)
        {
            var Branch = context.Branches.Find(id);
            context.Branches.Remove(Branch);
        }
        public async Task<Branch> FindBranch(int id)
        {
            return await context.Branches.FindAsync(id);
        }

        public async Task<IEnumerable<Branch>> GetBranchesAsync()
        {
            return await context.Branches
                .Include(b => b.AppUser)
                .Include(b => b.studyClasses)
                .Include(b => b.Students)
                .ToListAsync();
        }
    }
}
