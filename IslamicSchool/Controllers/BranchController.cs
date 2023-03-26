using AutoMapper;
using IslamicSchool.Data;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.DataTransferObjects.EditDtos;
using IslamicSchool.DataTransferObjects.GetDataDtos;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Controllers
{
    public class BranchController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly UserManager<AppUser> userManager;

        public BranchController(IUnitOfWork uow, IMapper mapper, DataContext context, UserManager<AppUser> userManager)
        { 
            this.uow = uow;
            this.mapper = mapper;
            this.context = context;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetALlBranches()
        {
            
            var branches = await uow.BranchRepository.GetBranchesAsync();
            var branch = mapper.Map<IEnumerable<GetBranchDto>>(branches);
            return Ok(branch);
        }
        [HttpGet("count")]
        public IActionResult GetBranchesCount()
        {

            var branches =  context.Branches.ToListAsync().Result.Count();
            return Ok(branches);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranchById(int id)
        {
            var branches = await context.Branches.Include(b => b.AppUsers)
                                                   .ThenInclude(u => u.UserRoles)
                                                   .ThenInclude(ur => ur.Role)
                                                   .Include(b => b.studyClasses)
                                                   .Include(b => b.Students)
                                                   .Where(x => x.Id == id)
                                                   .ToListAsync();

            var branchDtos = mapper.Map<List<GetBranchDto>>(branches);
            var adminUsers = new List<AppUser>();
            foreach (var branch in branches)
            {
                var admins = branch.AppUsers.Where(u => u.UserRoles.Any(ur => ur.Role.Name == "ADMIN"));
                adminUsers.AddRange(admins);
            }

            foreach (var branchDto in branchDtos)
            {
                branchDto.AppUsers = adminUsers;
            }

            return Ok(branchDtos);
        }
        [HttpGet("studyclasscount/{id}")]
        public async Task<IActionResult> GetStudyClassCount(int id)
        {
            var studyClassCount = await context.Branches
                .Where(x => x.Id == id)
                .Select(b => b.studyClasses.Count)
                .SingleOrDefaultAsync();

            return Ok(studyClassCount);
        }
        [HttpGet("students/{id}")]
        public async Task<IActionResult> GetStudentsCount(int id)
        {
            var studyClassCount = await context.Branches
                .Where(x => x.Id == id)
                .Select(b => b.Students.Count)
                .SingleOrDefaultAsync();

            return Ok(studyClassCount);
        }
        [HttpGet("studentslist/{id}")]
        public async Task<IActionResult> GetStudents(int id)
        {
            var studyClassCount = await context.Branches
                .Where(x => x.Id == id)
                .Include(b => b.Students)
                .ToListAsync();

            return Ok(studyClassCount);
        }
        [HttpGet("teachercount/{id}")]
        public async Task<IActionResult> GetTeacherCount(int id)
        {
            var teacherCount = await context.UserRoles
                .Where(ur => ur.Role.Name == "TEACHER" && ur.User.BranchId == id)
                .CountAsync();

            return Ok(teacherCount);
        }
        [HttpGet("teachers/{id}")]
        public async Task<IActionResult> GetBranchByIdWithTeachers(int id)
        {
            var branches = await context.Branches.Include(b => b.AppUsers)
                                                   .ThenInclude(u => u.UserRoles)
                                                   .ThenInclude(ur => ur.Role)
                                                   .Include(b => b.studyClasses)
                                                   .Include(b => b.Students)
                                                   .Where(x => x.Id == id)
                                                   .ToListAsync();

            var branchDtos = mapper.Map<List<GetBranchDto>>(branches);
            var adminUsers = new List<AppUser>();
            foreach (var branch in branches)
            {
                var admins = branch.AppUsers.Where(u => u.UserRoles.Any(ur => ur.Role.Name == "TEACHER"));
                adminUsers.AddRange(admins);
            }

            foreach (var branchDto in branchDtos)
            {
                branchDto.AppUsers = adminUsers;
            }

            return Ok(branchDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddBranch(BranchDto branchdto)
        {
            var branch = mapper.Map<Branch>(branchdto);
            uow.BranchRepository.AddBranch(branch);
            var appUser = await userManager.FindByIdAsync(branchdto.AppUserId.ToString());
            appUser.BranchId = branch.Id;
            appUser.Branch = branch;
            await userManager.UpdateAsync(appUser);
            await uow.SaveAsync();
            return Ok();
        }
        [HttpPost("Test")]
        public async Task<IActionResult> TestAdd(Branch branch)
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
        public async Task<IActionResult> UpdateBranch(int id, EditBranchDto branchDto, Guid? adminUserId)
        {
            var branch = await uow.BranchRepository.FindBranch(id);

            if (branch == null)
            {
                return NotFound();
            }

            // Update the branch data
            mapper.Map(branchDto, branch);

            // Update the admin user if a new admin user ID is provided
            if (adminUserId.HasValue)
            {
                // find the existing admin user associated with this branch
                var existingAdminUser = await userManager.Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .Where(u => u.BranchId == id && u.UserRoles.Any(ur => ur.Role.Name == "ADMIN"))
                    .FirstOrDefaultAsync();

                if (existingAdminUser != null)
                {
                    // remove the existing admin user from the branch's collection of app users
                    branch.AppUsers.Remove(existingAdminUser);

                    // clear the existing adm   in user's branch reference
                    existingAdminUser.Branch = null;
                    existingAdminUser.BranchId = null;

                    // update the existing admin user in the database
                    await userManager.UpdateAsync(existingAdminUser);
                }

                // find the new admin user associated with this branch
                var newAdminUser = await userManager.FindByIdAsync(adminUserId.Value.ToString());
                if (newAdminUser == null)
                {
                    return NotFound("Could not find user with the specified ID.");
                }

                // update the new admin user's branch information
                newAdminUser.BranchId = branch.Id;
                newAdminUser.Branch = branch;
                await userManager.UpdateAsync(newAdminUser);

                // add the new admin user to the branch's collection of app users
                branch.AppUsers.Add(newAdminUser);
            }

            // save changes to the database
            await uow.SaveAsync();

            // return the updated branch DTO
            var updatedBranchDto = mapper.Map<BranchDto>(branch);
            return Ok(updatedBranchDto);
        }


    }
}
