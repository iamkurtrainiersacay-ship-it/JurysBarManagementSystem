namespace JurysBarManagementSystem.Models.User
{
    public class PayrollRecord
    {
        public int Id { get; set; }

        public string? EmployeeName { get; set; }

        public string? Role { get; set; }

        public double Salary { get; set; }

        public double HoursWorked { get; set; }

        public double Bonus { get; set; }

        public double Deductions { get; set; }

        public double NetPay { get; set; }

        public string? Date { get; set; }
    }
}