using AutoMapper;
using IslamicSchool.DataTransferObjects.GetDataDtos;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IslamicSchool.Controllers
{
    public class TeacherTaskController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public TeacherTaskController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<TeacherTasksDto>> GetTeacherTasks()
        {
            var teacherTasks = await uow.TeacherTaskRepository.GetTeachersTaksAsync();
            var teachertaskdto = mapper.Map<IEnumerable<TeacherTasksDto>>(teacherTasks);
            return (teachertaskdto);
        }
        [HttpPost]
        public async Task<IActionResult> AddTask(TeacherTasksDto teacherTasksDto)
        {
            var teachertask = mapper.Map<TeacherTask>(teacherTasksDto);
            uow.TeacherTaskRepository.AddTeacherTasks(teachertask);
            await uow.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            uow.TeacherTaskRepository.DeleteTeacherTasks(id);
            await uow.SaveAsync();
            return Ok(id);
        }

    }
}
