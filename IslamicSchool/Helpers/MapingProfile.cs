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
                .ForMember(dest => dest.BranchAdminId, opt => opt.MapFrom(src => src.AppUserId))
                /*.ForMember(dest => dest.BranchAdmin, opt => opt.MapFrom(src => src.AppUser))*/
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.FatherName, opt => opt.MapFrom(src => src.AppUser.FatherName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
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
