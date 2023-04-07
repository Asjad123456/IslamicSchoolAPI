namespace IslamicSchool.Interfaces
{
    public interface IUnitOfWork
    {
        IBranchRepository BranchRepository { get; }
        IGuardianRepository GuardianRepository { get; }
        IStudentRepository StudentRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        ICourseRepository CourseRepository { get; }
        IStudyClassRepository StudyClassRepository { get; }
        IBranchAdminRepository BranchAdminRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        ITeacherTaskRepository TeacherTaskRepository { get; }
        IUserRepository UserRepository { get; }
        IDeanRepository DeanRepository { get; }
        IAttendanceRepository AttendanceRepository { get; }
        IStudentEducationRepository StudentEducationRepository { get; }
        IAdminTaskRepository AdminTaskRepository { get; }
        ISchoolRepository SchoolRepository { get; }
        Task<bool> SaveAsync();
    }
}
