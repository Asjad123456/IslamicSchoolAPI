using IslamicSchool.Data;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Repository
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly DataContext context;

        public SchoolRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddSchool(School school)
        {
            context.Schools.Add(school);
        }

        public void DeleteSchool(int id)
        {
            var school = context.Schools.Find(id);
            context.Schools.Remove(school);
        }

        public async Task<School> FindSchool(int id)
        {
            return await context.Schools.FindAsync(id);
        }

        public async Task<IEnumerable<School>> GetSchoolsAsync()
        {
            var schools = await context.Schools
             .Include(p => p.Branches)
             .Include(p => p.AppUsers)
             .Include(p => p.StudyClasses)
             .Include(p => p.Students)
             .ToListAsync();
            return schools;
        }

        public void UpdateSchool(School school)
        {
            context.Entry(school).State = EntityState.Modified;
        }
    }
}
