using AutoMapper;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IslamicSchool.Controllers
{
    public class TeacherController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public TeacherController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teacher = await uow.TeacherRepository.GetTeachersAsync();
            return Ok(teacher);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            var teacher = await uow.TeacherRepository.FindTeacher(id);
            return Ok(teacher);
        }
        [HttpPost]
        public async Task<IActionResult> AddTeacher(TeacherDto teacherdto)
        {
            var teacher = mapper.Map<Teacher>(teacherdto);
            uow.TeacherRepository.AddTeachers(teacher);
            await uow.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            uow.TeacherRepository.DeleteTeacher(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, TeacherDto teacherdto)
        {
            var teacher = await uow.TeacherRepository.FindTeacher(id);

            mapper.Map(teacherdto, teacher);
            await uow.SaveAsync();
            return Ok();
        }
    }
}
