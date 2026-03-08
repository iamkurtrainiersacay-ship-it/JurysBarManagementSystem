namespace JurysBarManagementSystem.Models.User
{
    public class Payment
    {
        public int Id { get; set; }

        public string? CustomerName { get; set; }

        public double Amount { get; set; }

        public string? Date { get; set; }
    }
}