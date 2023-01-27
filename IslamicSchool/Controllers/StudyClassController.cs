using AutoMapper;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IslamicSchool.Controllers
{
    public class StudyClassController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public StudyClassController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudyClass()
        {
            var studyclass = await uow.StudyClassRepository.GetStudyClassAsync();
            return Ok(studyclass);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            var studyclass = await uow.StudyClassRepository.FindStudyClass(id);
            return Ok(studyclass);
        }
        [HttpPost]
        public async Task<IActionResult> AddClass(StudyClassDto studyClassDto)
        {
            var studyclass = mapper.Map<StudyClass>(studyClassDto);
            uow.StudyClassRepository.AddStudyClass(studyclass);
            await uow.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            uow.StudyClassRepository.DeleteStudyClass(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudyClassDto studyClassDto)
        {
            var studyclass = await uow.StudyClassRepository.FindStudyClass(id);

            mapper.Map(studyClassDto, studyclass);
            await uow.SaveAsync();
            return Ok();
        }
    }
}
