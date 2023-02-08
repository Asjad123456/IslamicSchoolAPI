using AutoMapper;
using IslamicSchool.Data;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.DataTransferObjects.GetDataDtos;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using IslamicSchool.UOW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly DataContext context;

        public StudentController(IUnitOfWork uow, IMapper mapper, DataContext context)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await uow.StudentRepository.GetStudentsAsync();
            var studentdto = mapper.Map<IEnumerable<GetStudentDto>>(students);
            return Ok(studentdto);
        }
        [HttpGet("number")]
        public IActionResult GetStudentCount()
        {
            var student = uow.StudentRepository.CountStudents();
            return Ok(student);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentsById(int id)
        {
            var students = await context.Students.Include(b => b.Guardian)
                                                   .Where(x => x.id == id)
                                                   .ToListAsync();
            var studentDtos = mapper.Map<List<GetStudentDto>>(students);
            return Ok(studentDtos);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentDto studentDto)
        {
            var student = mapper.Map<Student>(studentDto);
            uow.StudentRepository.AddStudents(student);
            await uow.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            uow.StudentRepository.DeleteStudents(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentForUpdateDto studentForUpdate)
        {
            var student = await uow.StudentRepository.FindStudent(id);

            mapper.Map(studentForUpdate, student);
            await uow.SaveAsync();
            return Ok();
        }
    }
}
