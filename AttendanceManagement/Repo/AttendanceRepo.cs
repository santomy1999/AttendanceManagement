using AttendanceManagement.Models;
using AttendanceManagement.Repo.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.Repo
{
    public class AttendanceRepo:IAttendanceRepo
    {
        private EmployeeDbContext _context;
        public AttendanceRepo(EmployeeDbContext context)
        {
            _context = context;
        }
        public async Task<Attendance?> EmployeeCheckIn(int Id)
        {
           var emp = await GetEmployee(Id);
            if (emp == null) throw new Exception("Employee Id is invalid");
            var currentDate = DateTime.Now;
            var date = currentDate.Date;
            var records = await _context.Attendances.Where(att=>att.EmpId == Id).ToListAsync();
            var currentDateCheckins = records.Where(att=>att.CheckIn?.Date== date).ToList();
            if (currentDateCheckins.Count > 0)
            {
                throw new Exception("Checkin Already Exist for this date.");
            }
            else
            {
                var record = new Attendance();
                record.Id = await GenerateId();
                record.EmpId = Id;
                record.CheckIn = currentDate;
                await _context.Attendances.AddAsync(record);
                
                if(await _context.SaveChangesAsync() > 0)
                {
                    return record;
                }
                return null ;
            }
        }
        public async Task<Attendance?> EmployeeCheckOut(int Id)
        {
            var emp = await GetEmployee(Id);
            if (emp == null) throw new Exception("Employee Id is invalid");
            var currentDate = DateTime.Now;
            var date = currentDate.Date;
            var record = await _context.Attendances.FirstOrDefaultAsync(att => att.EmpId == Id && (att.CheckIn.HasValue ? att.CheckIn.Value.Date : null) == date);
            if (record==null)
            {
                throw new Exception("Not Checked in . Checkin before checking out");
            }
            else if(record.CheckOut != null)
            {
                throw new Exception("Already Checked Out");
            }
            else
            {
                record.CheckOut = currentDate;
                if (await _context.SaveChangesAsync() > 0)
                {
                    return record;
                }
                return null;
            }
        }
        public async Task<List<AttendanceDetails>> GetAttendanceDetails()
        {
            var attendanceList = await _context.Attendances.ToListAsync();
            var attendanceDetailsList = new List<AttendanceDetails>();
            foreach (var attendance in attendanceList)
            { 
                if (attendance.EmpId != null)
                {
                    Employee employee = await GetEmployee((int)attendance.EmpId);
                    AttendanceDetails attendanceDetails = new AttendanceDetails
                    {
                        Id = attendance.Id,
                        EmpId = employee.Id,
                        Name = employee.Name,
                        Designation = employee.Designation,
                        CheckIn = attendance.CheckIn,
                        CheckOut = attendance.CheckOut,

                    };
                    attendanceDetailsList.Add(attendanceDetails);
                }
                
            }
            return attendanceDetailsList;
        }
        public async Task<Employee?> GetEmployee(int Id)
        {
            return await _context.Employees.FindAsync(Id) ;
        }
        public async Task<int> GenerateId()
        {
            Random rnd = new Random();
            int num = rnd.Next();
            return num;
        }
    }
}
