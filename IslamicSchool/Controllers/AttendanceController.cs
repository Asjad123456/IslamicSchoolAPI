using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IslamicSchool.Data;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Controllers
{
    [ApiController]
    [Route("api/class/{classId}/attendance")]
    public class AttendanceController : ControllerBase
    {
        private readonly DataContext _context;

        public AttendanceController(DataContext context)
        {
            _context = context;
        }

/*        [HttpGet("{date}")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendance(int classId, DateTime date)
        {
            var attendance = await _context.Attendances
*//*                .Include(a => a.Student)*//*
                .Where(a => a.Student.StudyClassId == classId && a.Date == date)
                .ToListAsync();

            return attendance;
        }
        [HttpGet("attendance")]
        public async Task<IEnumerable<Attendance>> GetAttendanceForClassAndDate(int studyclassId, DateTime date)
        {
            var attendance = await _context.Attendances
                .Include(a => a.Student)
                .Where(a => a.StudyClassId == studyclassId && a.Date == date.Date)
                .ToListAsync();

            return attendance;
        }*/

        /* [HttpPost("{date}")]
         public async Task<IActionResult> MarkAttendance(int classId, DateTime date, List<AttendanceDto> attendanceDto)
         {
             var attendance = await _context.Attendances
                 .Include(a => a.Student)
                 .Where(a => a.Student.StudyClassId == classId && a.Date == date)
                 .ToListAsync();

             foreach (var item in attendance)
             {
                 var dto = attendanceDto.FirstOrDefault(a => a.StudentId == item.StudentId);
                 if (dto != null)
                 {
                     item.IsPresent = dto.IsPresent;
                 }
             }

             await _context.SaveChangesAsync();

             return Ok();
         }*/
        /*[HttpPost("postattendance")]
        public async Task<ActionResult> AddAttendanceForClass(int classId, DateTime date, List<Attendance> attendanceData)
        {
            // Delete any existing attendance data for this class and date
            var existingAttendance = await _context.Attendances
                .Where(a => a.Student.StudyClassId == classId && a.Date == date)
                .ToListAsync();

            _context.RemoveRange(existingAttendance);

            // Add the new attendance data
            foreach (var attendance in attendanceData)
            {
                attendance.Date = date;
                attendance.Student = await _context.Students.FindAsync(attendance.StudentId);
                _context.Attendances.Add(attendance);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }*/
        [HttpPost("attendance")]
        public async Task<ActionResult> AddAttendance(int classId, DateTime date, [FromBody] List<int> studentIds, bool isPresent = true)
        {
            var attendanceList = new List<Attendance>();
            foreach (var studentId in studentIds)
            {
                attendanceList.Add(new Attendance
                {
                    Date = date,
                    IsPresent = isPresent,
                    StudentId = studentId,
                    StudyClassId = classId
                });
            }

            _context.Attendances.AddRange(attendanceList);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("attendance")]
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
        }

        public class AttendanceResponse
        {
            public string StudentName { get; set; }
            public int StudentRollNumber { get; set; }
            public bool IsPresent { get; set; }
        }
        [HttpGet]
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
                IsPresent = a.IsPresent
            }).ToList();

            return attendanceResponseList;
        }

    }

}