public class Payment
{
    public int Id { get; internal set; }
    public string? Customer { get; internal set; }
    public double Amount { get; internal set; }
    public string? Date { get; internal set; }
    public object? CustomerName { get; internal set; }
}