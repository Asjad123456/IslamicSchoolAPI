using AutoMapper;
using IslamicSchool.DataTransferObjects.GetDataDtos;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IslamicSchool.Controllers
{
    public class AdminTaskController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public AdminTaskController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<AdminTaskDto>> GetAdminTasks(Guid appUserId)
        {
            var AdminTasks = await uow.AdminTaskRepository.GetAdminTaksAsync();
            var filteredTasks = AdminTasks.Where(t => t.AppUserId == appUserId);
            var Admintaskdto = mapper.Map<IEnumerable<AdminTaskDto>>(AdminTasks);
            return (Admintaskdto);
        }
        [HttpPost]
        public async Task<IActionResult> AddTask(AdminTaskDto adminTaskDto)
        {
            var admintask = mapper.Map<AdminTasks>(adminTaskDto);
            uow.AdminTaskRepository.AddAdminTasks(admintask);
            await uow.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            uow.AdminTaskRepository.DeleteAdminTasks(id);
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}
