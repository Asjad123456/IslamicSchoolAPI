using AutoMapper;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.DataTransferObjects.AddDtos;
using IslamicSchool.DataTransferObjects.EditDtos;
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
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId));
            CreateMap<Branch, EditBranchDto>()
                .ReverseMap();
                CreateMap<Branch, GetBranchDto>()
                   .ForMember(dto => dto.BranchAdminId, opt => opt.MapFrom(src => src.AppUserId))
                   .ForMember(dto => dto.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                   .ForMember(dto => dto.FatherName, opt => opt.MapFrom(src => src.AppUser.FatherName))
                   .ForMember(dto => dto.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                   .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                   .ForMember(dto => dto.StudyClasses, opt => opt.MapFrom(src => src.studyClasses))
                .ReverseMap();
            CreateMap<StudyClass, GetClassDto>()
                   .ForMember(dto => dto.TeacherId, opt => opt.MapFrom(src => src.AppUserId))
                   .ForMember(dto => dto.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                   .ForMember(dto => dto.FatherName, opt => opt.MapFrom(src => src.AppUser.FatherName))
                   .ForMember(dto => dto.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                   .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                   .ForMember(dto => dto.Students, opt => opt.MapFrom(src => src.Students))
                .ReverseMap();
            CreateMap<StudyClass, AddStudyClassDto>()
                   .ForMember(dto => dto.AppUserId, opt => opt.MapFrom(src => src.AppUserId))
                   .ForMember(dto => dto.BranchId, opt => opt.MapFrom(src => src.BranchId))
                .ReverseMap();
            CreateMap<Student, StudentForUpdateDto>().ReverseMap();
            CreateMap<Guardian, GuardianDto>().ReverseMap();
            CreateMap<TeacherDto, Teacher>()
                .ReverseMap()
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId));
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Guardian, opt => opt.MapFrom(src => src.GuardianId))
                .ReverseMap();
            CreateMap<Student, AddStudentDto>()
                .ForMember(dest => dest.GuardianId, opt => opt.MapFrom(src => src.GuardianId))
                .ForMember(dest => dest.StudyClassId, opt => opt.MapFrom(src => src.StudyClassId))
               .ReverseMap();
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
