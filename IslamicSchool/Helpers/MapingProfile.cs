using AutoMapper;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.DataTransferObjects.GetDataDtos;
using IslamicSchool.Entities;

namespace IslamicSchool.Helpers
{
    public class MapingProfile : Profile
    {
        public MapingProfile()
        {
            CreateMap<UserForRegistrationDto, AppUser>()
                .ReverseMap();
            CreateMap<Branch, BranchDto>()
                .ForMember(dest => dest.BranchAdminId, opt => opt.MapFrom(src => src.AppUsers.FirstOrDefault().Id))
                .ReverseMap();
            CreateMap<Student, StudentForUpdateDto>().ReverseMap();
            CreateMap<Guardian, GuardianDto>().ReverseMap();
            CreateMap<TeacherDto, Teacher>()
                .ReverseMap()
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId));
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Guardian, opt => opt.MapFrom(src => src.GuardianId))
                .ReverseMap();
            CreateMap<AddStudentDto, Student>()
                .ForMember(dest => dest.GuardianId, opt => opt.MapFrom(src => src.GuardianId));
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<StudyClassDto, StudyClass>()
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.TeacherId))
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId));
            CreateMap<BranchAdminDto, BranchAdmin>()
                .ReverseMap();
            CreateMap<QuestionsDto, Question>()
                .ReverseMap();
            CreateMap<TeacherTasksDto, TeacherTask>()
                .ReverseMap();
        }
    }
}
