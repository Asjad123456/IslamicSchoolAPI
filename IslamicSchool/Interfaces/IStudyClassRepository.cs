using IslamicSchool.Entities;

namespace IslamicSchool.Interfaces
{
    public interface IStudyClassRepository
    {
        Task<IEnumerable<StudyClass>> GetStudyClassAsync();
        void AddStudyClass(StudyClass studyClass);
        void DeleteStudyClass(int id);
        Task<StudyClass> FindStudyClass(int id);
    }
}
