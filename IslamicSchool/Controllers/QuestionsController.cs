using AutoMapper;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IslamicSchool.Controllers
{
    public class QuestionsController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public QuestionsController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await uow.QuestionRepository.GetQuestionsAsync();
            return Ok(questions);
        }
        [HttpGet("{id}")]

        [HttpPost]
        public async Task<IActionResult> AddQuestions(QuestionsDto questionDto)
        {
            var question = mapper.Map<Question>(questionDto);
            uow.QuestionRepository.AddQuestions(question);
            await uow.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            uow.QuestionRepository.DeleteQuestions(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, QuestionsDto questionsDto)
        {
            var question = await uow.QuestionRepository.FindQuestions(id);

            mapper.Map(questionsDto, question);
            await uow.SaveAsync();
            return Ok();
        }
    }
}
