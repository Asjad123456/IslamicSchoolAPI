using AutoMapper;
using IslamicSchool.Data;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.DataTransferObjects.GetDataDtos;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IslamicSchool.Controllers
{
    public class SchoolController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly UserManager<AppUser> userManager;

        public SchoolController(IUnitOfWork uow, IMapper mapper, DataContext context, UserManager<AppUser> userManager)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.context = context;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetSchools()
        {
            var schools = await uow.SchoolRepository.GetSchoolsAsync();
            var schooldto = mapper.Map<IEnumerable<GetSchoolDto>>(schools);
            return Ok(schooldto);
        }

        [HttpPost]
        public async Task<IActionResult> AddSchool(SchoolDto schooldto)
        {
            var school = mapper.Map<School>(schooldto);
            uow.SchoolRepository.AddSchool(school);
            var appUser = await userManager.FindByIdAsync(schooldto.AppUserId.ToString());
            appUser.SchoolId = school.id;
            appUser.School = school;
            await userManager.UpdateAsync(appUser);
            await uow.SaveAsync();
            return Ok();
        }
    }
}
