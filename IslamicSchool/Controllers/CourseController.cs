using AutoMapper;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IslamicSchool.Controllers
{
    public class CourseController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CourseController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var course = await uow.CourseRepository.GetCoursesAsync();
            return Ok(course);
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse(Course course)
        {
            uow.CourseRepository.AddCourse(course);
            await uow.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            uow.CourseRepository.DeleteCourse(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseDto courseDto)
        {
            var course = await uow.CourseRepository.FindCourse(id);

            mapper.Map(courseDto, course);
            await uow.SaveAsync();
            return Ok();
        }
    }
}
