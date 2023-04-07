using AutoMapper;
using IslamicSchool.Data;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.DataTransferObjects.GetDataDtos;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Controllers
{
    public class StudentEducationController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly DataContext context;

        public StudentEducationController(IUnitOfWork uow, IMapper mapper, DataContext context)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetEducation()
        {
            var education = await uow.StudentEducationRepository.GeEducationAsync();
            var educationDto = mapper.Map<IEnumerable<StudentEducationDto>>(education);
            return Ok(educationDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEducationById(int id)
        {
            var education = await context.StudentEducations
                                                   .Where(x => x.Id == id)
                                                   .ToListAsync();
            var educationtDtos = mapper.Map<List<StudentEducationDto>>(education);
            return Ok(educationtDtos);
        }
        [HttpPost]
        public async Task<IActionResult> AddEducation(StudentEducationDto educationDto)
        {
            var education = mapper.Map<StudentEducation>(educationDto);

            uow.StudentEducationRepository.AddEducation(education);
            await uow.SaveAsync();
            return Ok();

        }
    }
}
