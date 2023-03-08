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
        [HttpGet("count")]
        public IActionResult GetBranchesCount()
        {
            var students = context.Students.ToListAsync().Result.Count();
            return Ok(students);
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

            // Create a new guardian object and map its properties from the studentDto
            var guardian = new Guardian
            {
                Name = studentDto.GuardianName,
                ContactNumber = studentDto.GuardianContactNumber,
                Address = studentDto.GuardianAddress,
                FatherName = studentDto.FatherName
            };

            // Set the Guardian property of the student object to the newly created guardian object
            student.Guardian = guardian;

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

            // Update the properties of the student object using the studentForUpdate object
            mapper.Map(studentForUpdate, student);

            // Create a new guardian object and map its properties from the studentForUpdate
            var guardian = new Guardian
            {
                Name = studentForUpdate.GuardianName,
                ContactNumber = studentForUpdate.GuardianContactNumber,
                Address = studentForUpdate.GuardianAddress,
                FatherName = studentForUpdate.GuardianFatherName
            };

            // If the GuardianId property is not null, set it to the GuardianId property of the student object
            if (studentForUpdate.GuardianId.HasValue)
            {
                student.GuardianId = studentForUpdate.GuardianId.Value;
            }

            // Set the Guardian property of the student object to the newly created guardian object
            student.Guardian = guardian;

            await uow.SaveAsync();
            return Ok();
        }
    }
}
