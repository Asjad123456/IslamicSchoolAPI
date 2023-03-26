using AutoMapper;
using IslamicSchool.Data;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.DataTransferObjects.AddDtos;
using IslamicSchool.DataTransferObjects.EditDtos;
using IslamicSchool.DataTransferObjects.GetDataDtos;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Controllers
{
    public class StudyClassController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly UserManager<AppUser> userManager;

        public StudyClassController(IUnitOfWork uow, IMapper mapper, DataContext context, UserManager<AppUser> userManager)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.context = context;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudyClass()
        {
            var studyclass = await uow.StudyClassRepository.GetStudyClassAsync();
            return Ok(studyclass);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            var branches = await context.StudyClasses.Include(b => b.AppUser)
                                                   .Include(b => b.Students)
                                                   .Where(x => x.Id == id)
                                                   .ToListAsync();
            var branchDtos = mapper.Map<List<GetClassDto>>(branches);
            return Ok(branchDtos);
        }
        [HttpGet("studentcount/{id}")]
        public async Task<IActionResult> GetClassStudentsCount(int id)
        {
            var studentsCount = await context.Students
                                              .CountAsync(s => s.StudyClass.Id == id);
            return Ok(studentsCount);
        }
        [HttpPost]
        public async Task<IActionResult> AddClass(AddStudyClassDto addstudyClassDto)
        {
            var studyclass = mapper.Map<StudyClass>(addstudyClassDto);
            uow.StudyClassRepository.AddStudyClass(studyclass);
            var appUser = await userManager.FindByIdAsync(addstudyClassDto.AppUserId.ToString());
            appUser.BranchId = studyclass.BranchId;
            appUser.Branch = studyclass.Branch;
            await userManager.UpdateAsync(appUser);
            await uow.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            uow.StudyClassRepository.DeleteStudyClass(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, EditClassDto studyClassDto, Guid? appUserId)
        {
            var studyclass = await uow.StudyClassRepository.FindStudyClass(id);
            if(studyclass == null)
            {
                return NotFound();
            }

            mapper.Map(studyClassDto, studyclass);
            if (appUserId.HasValue)
            {
                var existingUser = await userManager.FindByIdAsync(studyclass.AppUserId.ToString());

                if(existingUser != null)
                {
                    existingUser.StudyClasses.Remove(studyclass);
                    studyclass.AppUserId = Guid.Empty;
                    studyclass.AppUser = null;

                    await userManager.UpdateAsync(existingUser);

                }

                var newTeacher = await userManager.FindByIdAsync(appUserId.Value.ToString());
                if(newTeacher == null)
                {
                    return NotFound("Couldnt find user with specifies id");
                }

                newTeacher.StudyClasses.Add(studyclass);
                await userManager.UpdateAsync(newTeacher);

                studyclass.AppUserId = newTeacher.Id;
                studyclass.AppUser = newTeacher;

            }
            await uow.SaveAsync();

            var updatedClassDto = mapper.Map<EditClassDto>(studyclass);
            return Ok(updatedClassDto);
        }
    }
}
