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
        [HttpPost("attendance")]
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
            public DateTime Date { get; set; }
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
                IsPresent = a.IsPresent,
                Date = a.Date
            }).ToList();

            return attendanceResponseList;
        }

    }

}