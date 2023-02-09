using AutoMapper;
using IslamicSchool.DataTransferObjects.EditDtos;
using IslamicSchool.DataTransferObjects.GetDataDtos;
using IslamicSchool.Entities;
using IslamicSchool.Extensions;
using IslamicSchool.Helpers;
using IslamicSchool.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace IslamicSchool.Controllers
{
    public class UserController : BaseController
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;

        public UserController(Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork uow)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.uow = uow;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers([FromQuery]UserParams userParams)
        {
            var users = await uow.UserRepository.GetUsers(userParams);
            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await userManager.Users.Include(b => b.Branch)
                                              .Where(x => x.Id == id)
                                              .ToListAsync();
            return Ok(user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> AddBranchForUser(Guid id, AddBranchForSupervisorDto addBranchForSupervisorDto)
        {
            var user = await userManager.Users.Where(x => x.Id == id)
                                                    .FirstAsync();
            user.BranchId = addBranchForSupervisorDto.BranchId;
            var result = await userManager.UpdateAsync(user);
            return Ok(result);
        }
        [HttpGet("teachers")]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await userManager.GetUsersInRoleAsync("TEACHER");
            return Ok(teachers);
        }
        [HttpGet("admins")]
        public async Task<IActionResult> GetAdmins()
        {
            var teachers = await userManager.GetUsersInRoleAsync("ADMIN");
            return Ok(teachers);
        }
        [HttpGet("teachers-count")]
        public IActionResult GetTeachersCount()
        {
            var teachers = userManager.GetUsersInRoleAsync("TEACHER").Result.Count;
            return Ok(teachers);
        }
        [HttpGet("admins-count")]
        public IActionResult GetAdminsCount()
        {
            var admins = userManager.GetUsersInRoleAsync("ADMIN").Result.Count;
            return Ok(admins);
        }
    }
}
