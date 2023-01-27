using IslamicSchool.Data;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Repository
{
    public class GuardianRepository : IGuardianRepository
    {
        private readonly DataContext context;

        public GuardianRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddGuardian(Guardian guardian)
        {
            context.Guardians.Add(guardian);
        }

        public void DeleteGuardian(int id)
        {
            var guardian = context.Guardians.Find(id);
            context.Guardians.Remove(guardian);
        }

        public async Task<Guardian> FindGuardian(int id)
        {
            return await context.Guardians.FindAsync(id);
        }

        public async Task<IEnumerable<Guardian>> GetGuardianAsync()
        {
            return await context.Guardians.ToListAsync();
        }
    }
}
