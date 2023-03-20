using IslamicSchool.Data;
using IslamicSchool.Entities;
using IslamicSchool.Interfaces;

namespace IslamicSchool.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly DataContext context;

        public AttendanceRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddAttendance(Attendance attendance)
        {
            context.Attendances.Add(attendance);
        }
    }
}
