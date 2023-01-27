using AutoMapper;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IslamicSchool.Controllers
{
    public class BranchAdminController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public BranchAdminController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetBranchAdmins()
        {
            var branchadmin = await uow.BranchAdminRepository.GetBranchAdminsAsync();
            return Ok(branchadmin);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranchAdminById(int id)
        {
            var branchadmin = await uow.BranchAdminRepository.FindBranchAdmin(id);
            return Ok(branchadmin);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(BranchAdminDto branchAdminDto)
        {
            var branchadmin = mapper.Map<BranchAdmin>(branchAdminDto);
            uow.BranchAdminRepository.AddBranchAdmin(branchadmin);
            await uow.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranchAdmin(int id)
        {
            uow.BranchAdminRepository.DeleteBranchAdmin(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranchAdmin(int id, BranchAdminDto branchAdminDto)
        {
            var branchadmin = await uow.StudentRepository.FindStudent(id);

            mapper.Map(branchAdminDto, branchadmin);
            await uow.SaveAsync();
            return Ok();
        }
    }
}
