using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IslamicSchool.Data;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Controllers
{
    public class AttendanceController : BaseController
    {
        private readonly DataContext _context;
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;

        public AttendanceController(DataContext context, IMapper mapper, IUnitOfWork uow)
        {
            _context = context;
            this.mapper = mapper;
            this.uow = uow;
        }
        /* [HttpPost("attendance")]
         public async Task<ActionResult> AddAttendance(List<AddAttendanceDto> attendanceList)
         {
             foreach (var attendance in attendanceList)
             {
                 var student = await uow.StudentRepository.FindStudent(attendance.StudentId);
                 if (student == null)
                 {
                     return BadRequest("Invalid student id.");
                 }

                 var studyClass = await uow.StudyClassRepository.FindStudyClass(attendance.StudyClassId);
                 if (studyClass == null)
                 {
                     return BadRequest("Invalid study class id.");
                 }

                 var addattendance = mapper.Map<Attendance>(attendance);
                 addattendance.Student = student;
                 addattendance.StudyClass = studyClass;

                 uow.AttendanceRepository.AddAttendance(addattendance);
             }

             await uow.SaveAsync(); 

             return Ok();
         }*/

        /*        [HttpGet("attendance")]
                public async Task<ActionResult<List<AttendanceResponse>>> GetAttendance(int classId, DateTime date)
                {
                    var attendance = await _context.Attendances
                        .Include(a => a.Student)
                        .Where(a => a.StudyClassId == classId && a.Date == date)
                        .ToListAsync();

                    var attendanceResponseList = attendance.Select(a => new AttendanceResponse
                    {
                        StudentName = a.Student.Name,
                        StudentRollNumber = a.Student.RollNumber,
                        IsPresent = a.IsPresent
                    }).ToList();

                    return attendanceResponseList;
                }*/

        /*        public class AttendanceResponse
                {
                    public string StudentName { get; set; }
                    public int StudentRollNumber { get; set; }
                    public bool IsPresent { get; set; }
                    public DateTime Date { get; set; }
                }*/
        /*        [HttpGet]
                public async Task<ActionResult<List<AttendanceResponse>>> GetAllAttendance(int classId)
                {
                    var attendance = await _context.Attendances
                        .Include(a => a.Student)
                        .Where(a => a.StudyClassId == classId)
                        .ToListAsync();

                    var attendanceResponseList = attendance.Select(a => new AttendanceResponse
                    {
                        StudentName = a.Student.Name,
                        StudentRollNumber = a.Student.RollNumber,
                        IsPresent = a.IsPresent,
                        Date = a.Date
                    }).ToList();

                    return attendanceResponseList;
                }*/

        /* [HttpPost]
         public async Task<ActionResult<Attendance>> PostAttendance(int classId, DateTime date, [FromBody] AttendanceRequestModel attendancePostModel)
         {
             var studyClass = await _context.StudyClasses
                                            .Include(sc => sc.Students)
                                            .FirstOrDefaultAsync(sc => sc.Id == classId);
             if (studyClass == null)
             {
                 return NotFound();
             }
             // Create attendance object
             var attendance = new Attendance
             {
                 ClassId = studyClass.Id,
                 Date = date,
                 Class = studyClass,
                 Records = new List<AttendanceRecord>()
             };
             // Iterate through studentIds and isPresentList to create attendance records
             for (int i = 0; i < attendancePostModel.StudentIds.Count; i++)
             {
                 var student = studyClass.Students.FirstOrDefault(s => s.id == attendancePostModel.StudentIds[i]);
                 if (student != null)
                 {
                     attendance.Records.Add(new AttendanceRecord
                     {
                         StudentId = student.id,
                         Student = student,
                         IsPresent = attendancePostModel.IsPresentList[i]
                     });
                 }
             }
             // Add attendance to database
             _context.Attendances.Add(attendance);
             await _context.SaveChangesAsync();
             return attendance;
         }

         [HttpGet("attendance")]
         public async Task<ActionResult<List<AttendanceRecord>>> GetAttendance(DateTime date, int classId)
         {
             var attendance = await _context.Attendances
                 .Include(a => a.Records)
                 .SingleOrDefaultAsync(a => a.Date == date && a.ClassId == classId);

             if (attendance == null)
             {
                 return NotFound();
             }

             return Ok(attendance.Records);
         }*/
        [HttpPost("{classId}/{date}")]
        public IActionResult AddAttendance(int classId, DateTime date, [FromBody] AttendanceRecordDto[] attendanceRecordsDto)
        {
            var attendanceRecords = new List<AttendanceRecord>();
            foreach (var attendanceRecordDto in attendanceRecordsDto)
            {
                attendanceRecords.Add(new AttendanceRecord
                {
                    StudentId = attendanceRecordDto.StudentId,
                    IsPresent = attendanceRecordDto.IsPresent
                });
            }

            var attendance = new Attendance
            {
                StudyClassId = classId,
                Date = date,
                AttendanceRecords = attendanceRecords
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            // Update the attendance records for each student
            foreach (var attendanceRecord in attendance.AttendanceRecords)
            {
                var student = _context.Students.FirstOrDefault(s => s.id == attendanceRecord.StudentId);
                if (student != null)
                {
                    var studentAttendance = new StudentAttendance
                    {
                        AttendanceId = attendance.Id,
                        StudentId = student.id,
                        IsPresent = attendanceRecord.IsPresent
                    };
                                                                                                                    
                    _context.StudentAttendances.Add(studentAttendance);
                }
            }

            _context.SaveChanges();

            return Ok();
        }
        [HttpGet("{classId}/{date}")]
        public IActionResult GetAttendance(int classId, DateTime date)
        {
            // Retrieve attendance record for the specified class and date
            var attendance = _context.Attendances
                .Include(a => a.AttendanceRecords)
                .ThenInclude(ar => ar.Student)
                .FirstOrDefault(a => a.StudyClassId == classId && a.Date == date);

            if (attendance == null)
            {
                return NotFound();
            }

            // Map attendance records to DTOs
            var attendanceRecordsDto = attendance.AttendanceRecords
                .Select(ar => new AttendanceRecordDto
                {
                    StudentId = ar.StudentId,
                    IsPresent = ar.IsPresent
                })
                .ToList();

            // Return attendance DTO
            var attendanceDto = new AttendanceDto
            {
                Date = attendance.Date,
                StudyClassId = attendance.StudyClassId,
                AttendanceRecords = attendanceRecordsDto
            };

            return Ok(attendanceDto);
        }

    }

}