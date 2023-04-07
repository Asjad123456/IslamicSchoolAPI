using IslamicSchool.Data;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Repository
{
    public class StudentEducationRepository : IStudentEducationRepository
    {
        private readonly DataContext context;

        public StudentEducationRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddEducation(StudentEducation studentEducation)
        {
            context.StudentEducations.Add(studentEducation);
        }

        public void DeleteEducation(int id)
        {
            var education = context.StudentEducations.Find(id);
            context.StudentEducations.Remove(education);
        }

        public async Task<IEnumerable<StudentEducation>> GeEducationAsync()
        {
            var education = await context.StudentEducations.ToListAsync();
            return education;
        }

        public void UpdateEducation(StudentEducation studentEducation)
        {
            context.Entry(studentEducation).State = EntityState.Modified;
        }
    }
}
