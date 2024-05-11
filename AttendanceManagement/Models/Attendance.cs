using System;
using System.Collections.Generic;

namespace AttendanceManagement.Models;

public partial class Attendance
{
    public int Id { get; set; }

    public int? EmpId { get; set; }

    public DateTime? CheckIn { get; set; }

    public DateTime? CheckOut { get; set; }
}
