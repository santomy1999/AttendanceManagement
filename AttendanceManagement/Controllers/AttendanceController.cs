using AttendanceManagement.Models;
using AttendanceManagement.Repo.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceRepo _attendanceRepo;
        public AttendanceController(IAttendanceRepo attendanceRepo)
        {
            _attendanceRepo = attendanceRepo;
        }
        [HttpPost("CheckId/{Id}")]
        public async Task<IActionResult> EmployeeCheckIn(int Id)
        {
            try
            {
                var res = await _attendanceRepo.EmployeeCheckIn(Id);
                if (res == null)
                {
                    return BadRequest("Unkown Error Occured");
                }
                return Ok("Checked in \n Time:" + res.CheckIn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("CheckOut/{Id}")]
        public async Task<IActionResult> EmployeeCheckOut(int Id)
        {
            try
            {
                var res = await _attendanceRepo.EmployeeCheckOut(Id);
                if (res == null)
                {
                    return BadRequest("Unkown Error Occured");
                }
                return Ok("Checked Out \nCheckIn Time:" + res.CheckIn + "\nCheckOut Time:" + res.CheckOut);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAttendanceDetails")]
        public async Task<IActionResult> GetAttendanceDetails()
        {
            try
            {
                var res = await _attendanceRepo.GetAttendanceDetails();
                if (res.Any())
                {
                    return Ok(res);
                }
                return NotFound("No records found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
