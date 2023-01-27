using IslamicSchool.Entities;

namespace IslamicSchool.Interfaces
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetQuestionsAsync();
        void AddQuestions(Question question);
        void DeleteQuestions(int id);
        Task<Question> FindQuestions(int id);
    }
}
