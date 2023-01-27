using IslamicSchool.Data;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Repository
{
    public class StudyClassRepository : IStudyClassRepository
    {
        private readonly DataContext context;

        public StudyClassRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddStudyClass(StudyClass studyClass)
        {
            context.StudyClasses.Add(studyClass);
        }

        public void DeleteStudyClass(int id)
        {
            var studyClass = context.StudyClasses.Find(id);
            context.StudyClasses.Remove(studyClass);
        }

        public async Task<StudyClass> FindStudyClass(int id)
        {
            return await context.StudyClasses.FindAsync(id);
        }

        public async Task<IEnumerable<StudyClass>> GetStudyClassAsync()
        {
            return await context.StudyClasses.ToListAsync();
        }
    }
}
