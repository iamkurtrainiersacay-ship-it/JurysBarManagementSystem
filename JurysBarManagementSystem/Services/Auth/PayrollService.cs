using System;
using System.Collections.Generic;
using System.Data.SQLite;
using JurysBarManagementSystem.Database;
using JurysBarManagementSystem.Models.User;

namespace JurysBarManagementSystem.Services
{
    public static class PayrollService
    {
        // =============================
        // Create Payroll
        // =============================

        public static PayrollRecord CreatePayroll(
            string employeeName,
            string role,
            double salaryRate,
            double hoursWorked,
            double bonus = 0,
            double otherDeduction = 0)
        {
            double grossPay = salaryRate * hoursWorked;

            // Philippine contribution estimation
            double sss = grossPay * 0.045;
            double philhealth = grossPay * 0.02;

            double totalDeduction = sss + philhealth + otherDeduction;

            double netPay = grossPay + bonus - totalDeduction;

            PayrollRecord payroll = new PayrollRecord
            {
                EmployeeName = employeeName,
                Role = role,
                Salary = salaryRate,
                HoursWorked = hoursWorked,
                Bonus = bonus,
                Deductions = totalDeduction,
                NetPay = netPay,
                Date = DateTime.Now.ToString("yyyy-MM-dd")
            };

            SavePayroll(payroll);

            return payroll;
        }

        // =============================
        // Save Payroll SQLite
        // =============================

        public static void SavePayroll(PayrollRecord p)
        {
            using var conn = SQLiteService.GetConnection();
            conn.Open();

            string sql = @"
INSERT INTO Payroll
(EmployeeName,Role,Salary,HoursWorked,Bonus,Deductions,NetPay,Date)
VALUES
(@name,@role,@salary,@hours,@bonus,@deduction,@net,@date)
";

            using var cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.AddWithValue("@name", p.EmployeeName);
            cmd.Parameters.AddWithValue("@role", p.Role);
            cmd.Parameters.AddWithValue("@salary", p.Salary);
            cmd.Parameters.AddWithValue("@hours", p.HoursWorked);
            cmd.Parameters.AddWithValue("@bonus", p.Bonus);
            cmd.Parameters.AddWithValue("@deduction", p.Deductions);
            cmd.Parameters.AddWithValue("@net", p.NetPay);
            cmd.Parameters.AddWithValue("@date", p.Date);

            cmd.ExecuteNonQuery();
        }

        // =============================
        // Update Payroll
        // =============================

        public static void UpdatePayroll(PayrollRecord p)
        {
            using var conn = SQLiteService.GetConnection();
            conn.Open();

            string sql = @"
UPDATE Payroll SET
EmployeeName=@name,
Role=@role,
Salary=@salary,
HoursWorked=@hours,
Bonus=@bonus,
Deductions=@deduction,
NetPay=@net
WHERE Id=@id
";

            using var cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", p.Id);
            cmd.Parameters.AddWithValue("@name", p.EmployeeName);
            cmd.Parameters.AddWithValue("@role", p.Role);
            cmd.Parameters.AddWithValue("@salary", p.Salary);
            cmd.Parameters.AddWithValue("@hours", p.HoursWorked);
            cmd.Parameters.AddWithValue("@bonus", p.Bonus);
            cmd.Parameters.AddWithValue("@deduction", p.Deductions);
            cmd.Parameters.AddWithValue("@net", p.NetPay);

            cmd.ExecuteNonQuery();
        }

        // =============================
        // Get Payroll List
        // =============================

        public static List<PayrollRecord> GetPayrolls()
        {
            List<PayrollRecord> list = new();

            using var conn = SQLiteService.GetConnection();
            conn.Open();

            using var cmd = new SQLiteCommand("SELECT * FROM Payroll", conn);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new PayrollRecord
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    EmployeeName = reader["EmployeeName"].ToString(),
                    Role = reader["Role"].ToString(),
                    Salary = Convert.ToDouble(reader["Salary"]),
                    HoursWorked = Convert.ToDouble(reader["HoursWorked"]),
                    Bonus = Convert.ToDouble(reader["Bonus"]),
                    Deductions = Convert.ToDouble(reader["Deductions"]),
                    NetPay = Convert.ToDouble(reader["NetPay"]),
                    Date = reader["Date"].ToString()
                });
            }

            return list;
        }
    }
}