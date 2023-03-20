using IslamicSchool.Data;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using IslamicSchool.Repository;
using Microsoft.AspNetCore.Identity;

namespace IslamicSchool.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;
        private readonly UserManager<AppUser> userManager;

        public UnitOfWork(DataContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public IBranchRepository BranchRepository => new BranchRepository(context);

        public IGuardianRepository GuardianRepository =>  new GuardianRepository(context);

        public IStudentRepository StudentRepository =>  new StudentRepository(context);

        public ITeacherRepository TeacherRepository => new TeacherRepository(context);

        public ICourseRepository CourseRepository => new CourseRepository(context);

        public IStudyClassRepository StudyClassRepository => new StudyClassRepository(context);

        public IBranchAdminRepository BranchAdminRepository => new BranchAdminRepository(context);

        public IQuestionRepository QuestionRepository => new QuestionsRepository(context);

        public ITeacherTaskRepository TeacherTaskRepository => new TeacherTaskRepository(context);

        public IUserRepository UserRepository => new UserRepository(userManager);

        public IDeanRepository DeanRepository => new DeanRepository(userManager);

        public IAttendanceRepository AttendanceRepository => new AttendanceRepository(context);

        public async Task<bool> SaveAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
