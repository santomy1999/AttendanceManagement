
using AttendanceManagement.Models;

namespace AttendanceManagement.Repo.Interface
{
    public interface IAttendanceRepo
    {
        Task<Attendance?> EmployeeCheckIn(int Id);
        Task<Attendance?> EmployeeCheckOut(int Id);
        Task<List<AttendanceDetails>> GetAttendanceDetails();
    }
}
