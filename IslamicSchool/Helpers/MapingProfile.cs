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
            CreateMap<School, SchoolDto>()
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUsers.FirstOrDefault().Id));
            CreateMap<Branch, BranchDto>()
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUsers.FirstOrDefault().Id))
                .ForMember(dest => dest.SchoolId, opt => opt.MapFrom(src => src.SchoolId));
            CreateMap<Branch, EditBranchDto>()
                .ReverseMap();
                CreateMap<Branch, GetBranchDto>()
                   .ForMember(dto => dto.AppUsers, opt => opt.MapFrom(src => src.AppUsers))
                .ReverseMap();
            CreateMap<StudyClass, GetClassDto>()
                   .ForMember(dto => dto.TeacherId, opt => opt.MapFrom(src => src.AppUserId))
                   .ForMember(dto => dto.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                   .ForMember(dto => dto.FatherName, opt => opt.MapFrom(src => src.AppUser.FatherName))
                   .ForMember(dto => dto.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                   .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                   .ForMember(dto => dto.Students, opt => opt.MapFrom(src => src.Students))
                   .ForMember(dto => dto.Address, opt => opt.MapFrom(src => src.AppUser.Address))
                .ReverseMap();
            CreateMap<StudyClass, AddStudyClassDto>()
                   .ForMember(dto => dto.AppUserId, opt => opt.MapFrom(src => src.AppUserId))
                   .ForMember(dto => dto.BranchId, opt => opt.MapFrom(src => src.BranchId))
                   .ForMember(dest => dest.SchoolId, opt => opt.MapFrom(src => src.SchoolId))
                .ReverseMap();
            CreateMap<StudyClass, EditClassDto>()
                 .ReverseMap();
            CreateMap<Student, GetStudentDto>()
                .ForMember(dto => dto.GuardianId, opt => opt.MapFrom(src => src.GuardianId))
                .ForMember(dto => dto.GuardianName, opt => opt.MapFrom(src => src.Guardian.Name))
                .ForMember(dto => dto.GuardianFatherName, opt => opt.MapFrom(src => src.Guardian.FatherName))
                .ForMember(dto => dto.ContactNumber, opt => opt.MapFrom(src => src.Guardian.ContactNumber))
                .ForMember(dto => dto.GuardianAddress, opt => opt.MapFrom(src => src.Guardian.Address))
                .ForMember(dto => dto.CNIC, opt => opt.MapFrom(src => src.Guardian.CNIC))
                .ForMember(dto => dto.StudentEducationId, opt => opt.MapFrom(src => src.StudentEducationId))
                .ForMember(dto => dto.CurrentStudyLevel, opt => opt.MapFrom(src => src.StudentEducation.CurrentStudyLevel))
                .ForMember(dto => dto.MarksInMatric, opt => opt.MapFrom(src => src.StudentEducation.MarksInMatric))
                .ForMember(dto => dto.MarksInIntermedicate, opt => opt.MapFrom(src => src.StudentEducation.MarksInIntermedicate))
                .ForMember(dto => dto.Remarks, opt => opt.MapFrom(src => src.StudentEducation.Remarks))
                .ReverseMap();
            CreateMap<Student, StudentForUpdateDto>().ReverseMap();
            CreateMap<Guardian, GuardianDto>().ReverseMap();
            CreateMap<TeacherDto, Teacher>()
                .ReverseMap()
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId));
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Guardian, opt => opt.MapFrom(src => src.GuardianId))
                .ForMember(dto => dto.StudentEducation, opt => opt.MapFrom(src => src.StudentEducationId))
                .ReverseMap();
            CreateMap<Student, AddStudentDto>()
                .ForMember(dest => dest.GuardianId, opt => opt.MapFrom(src => src.GuardianId))
                .ForMember(dto => dto.StudentEducationId, opt => opt.MapFrom(src => src.StudentEducationId))
                .ForMember(dest => dest.StudyClassId, opt => opt.MapFrom(src => src.StudyClassId))
                .ForMember(dest => dest.SchoolId, opt => opt.MapFrom(src => src.SchoolId))
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
            CreateMap<AdminTaskDto, AdminTasks>()
                .ReverseMap();
            CreateMap<AttendanceRecord, AttendanceRecordDto>()
                .ReverseMap();
            CreateMap<Attendance, AttendanceDto>()
                .ForMember(dest => dest.StudyClassId, opt => opt.MapFrom(src => src.StudyClass.Id))
                .ForMember(dest => dest.AttendanceRecords, opt => opt.MapFrom(src => src.AttendanceRecords))
                .ReverseMap();
            CreateMap<StudentAttendanceDto, StudentAttendance>()
                .ReverseMap();
            CreateMap<StudentEducationDto, StudentEducation>()
                .ReverseMap();
            /*            CreateMap<Attendance, AddAttendanceDto>()
                            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                            .ForMember(dest => dest.StudyClassId, opt => opt.MapFrom(src => src.StudyClassId))
                            .ReverseMap();*/

        }
    }
}
