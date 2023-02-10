using AutoMapper;
using IslamicSchool.Data;
using IslamicSchool.DataTransferObjects;
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
        private readonly DataContext context;

        public UserController(Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork uow, DataContext context)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.uow = uow;
            this.context = context;
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
                                              .Select(x => new AppUserDto
                                              {
                                                  id = x.Id,
                                                  UserName = x.UserName,
                                                  Email = x.Email,
                                                  BranchId = x.Branch.Id,
                                                  BranchName = x.Branch.BranchName,
                                                  Address = x.Branch.Address,
                                              })
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
            var filteredTeachers = teachers.Where(t => t.BranchId == null && t.Branch == null);
            List<AppUser> result = new List<AppUser>();
            foreach (var teacher in filteredTeachers)
            {
                var relatedBranch = await context.Branches.FirstOrDefaultAsync(b => b.AppUserId == teacher.Id);
                if (relatedBranch == null)
                {
                    result.Add(teacher);
                }
            }
            return Ok(result);
        }
        [HttpGet("supervisors")]
        public async Task<IActionResult> GetSupervisors()
        {
            var teachers = await userManager.GetUsersInRoleAsync("ADMIN");
            var filteredTeachers = userManager.Users
                                           .Include(t => t.Branch)
                                           .Where(t => t.UserRoles.Any(r => r.Role.Name == "ADMIN"))
                                           .ToList();
            return Ok(filteredTeachers);
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
