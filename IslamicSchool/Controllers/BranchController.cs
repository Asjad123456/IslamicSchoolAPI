using AutoMapper;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IslamicSchool.Controllers
{
    public class BranchController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public BranchController(IUnitOfWork uow, IMapper mapper)
        { 
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetALlBranches()
        {
            var branches = await uow.BranchRepository.GetBranchesAsync();
            return Ok(branches);
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetBranchType()
        {
            var BranchTypes = await uow.BranchRepository.GetBranchesAsync();
            var BranchTypeDto = mapper.Map<IEnumerable<KeyValueDto>>(BranchTypes);
            return Ok(BranchTypeDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranchById(int id)
        {
            var branch = await uow.BranchRepository.FindBranch(id);
            return Ok(branch);
        }
        [HttpPost]
        public async Task<IActionResult> AddBranch(Branch branch)
        {
            uow.BranchRepository.AddBranch(branch);
            await uow.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            uow.BranchRepository.DeleteBranch(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(int id, BranchDto branchDto)
        {
            var branch = await uow.BranchRepository.FindBranch(id);

            mapper.Map(branchDto, branch);
            await uow.SaveAsync();
            return Ok();
        }
    }
}
