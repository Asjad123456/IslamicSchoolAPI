using AutoMapper;
using IslamicSchool.Data;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.DataTransferObjects.AddDtos;
using IslamicSchool.DataTransferObjects.EditDtos;
using IslamicSchool.DataTransferObjects.GetDataDtos;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Controllers
{
    public class StudyClassController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly DataContext context;

        public StudyClassController(IUnitOfWork uow, IMapper mapper, DataContext context)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.context = context;
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
            var branches = await context.StudyClasses.Include(b => b.AppUser)
                                                   .Include(b => b.Students)
                                                   .Where(x => x.Id == id)
                                                   .ToListAsync();
            var branchDtos = mapper.Map<List<GetClassDto>>(branches);
            return Ok(branchDtos);
        }
        [HttpPost]
        public async Task<IActionResult> AddClass(AddStudyClassDto addstudyClassDto)
        {
            var studyclass = mapper.Map<StudyClass>(addstudyClassDto);
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
        public async Task<IActionResult> UpdateStudent(int id, EditClassDto studyClassDto)
        {
            var studyclass = await uow.StudyClassRepository.FindStudyClass(id);

            mapper.Map(studyClassDto, studyclass);
            await uow.SaveAsync();
            return Ok();
        }
    }
}
