using IslamicSchool.Data;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Repository
{
    public class QuestionsRepository : IQuestionRepository
    {
        private readonly DataContext context;

        public QuestionsRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddQuestions(Question question)
        {
            context.Questions.Add(question);
        }

        public void DeleteQuestions(int id)
        {
            var question = context.Questions.Find(id);
            context.Questions.Remove(question);
        }

        public async Task<Question> FindQuestions(int id)
        {
            return await context.Questions.FindAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            return await context.Questions.ToListAsync();
        }
    }
}
