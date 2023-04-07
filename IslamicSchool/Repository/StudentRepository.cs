    using IslamicSchool.Data;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext context;

        public StudentRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddStudents(Student student)
        {
            context.Students.Add(student);
        }

        public void DeleteStudents(int id)
        {
            var student = context.Students.Find(id);
            context.Students.Remove(student);
        }

        public async Task<Student> FindStudent(int id)
        {
            return await context.Students.FindAsync(id);
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            var students = await context.Students
                .Include(p => p.Guardian)
                .Include(p => p.StudentEducation)
                .ToListAsync();
            return students;
        }
        public void UpdateStudents(Student student)
        {
            context.Entry(student).State = EntityState.Modified;
        }
        public int GetStudentAmount()
        {
            var students =  context.Students.Count();
            return students;
        }

        public int CountStudents()
        {
           return context.Students.Count();
        }
    }
}
