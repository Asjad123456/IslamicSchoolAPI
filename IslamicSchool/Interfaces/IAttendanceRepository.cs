using IslamicSchool.Entities;

namespace IslamicSchool.Interfaces
{
    public interface IAttendanceRepository
    {
        void AddAttendance(Attendance attendance);
    }
}
