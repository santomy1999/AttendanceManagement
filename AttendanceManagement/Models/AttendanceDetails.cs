namespace AttendanceManagement.Models
{
    public class AttendanceDetails
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public DateTime? CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }
    }
}
