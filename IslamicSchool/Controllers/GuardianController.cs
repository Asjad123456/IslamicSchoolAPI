using AutoMapper;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IslamicSchool.Controllers
{

    public class GuardianController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public GuardianController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetGaurdians()
        {
            var guardian = await uow.GuardianRepository.GetGuardianAsync();
            return Ok(guardian);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuardianById(int id)
        {
            var guardian = await uow.GuardianRepository.FindGuardian(id);
            return Ok(guardian);
        }
        [HttpPost]
        public async Task<IActionResult> AddGuadrian(Guardian guardian)
        {
            uow.GuardianRepository.AddGuardian(guardian);
            await uow.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuardian(int id)
        {
            uow.GuardianRepository.DeleteGuardian(id);
            await uow.SaveAsync();
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuardian(int id, GuardianDto guardianDto)
        {
            var guardian = await uow.GuardianRepository.FindGuardian(id);

            mapper.Map(guardianDto, guardian);
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}
