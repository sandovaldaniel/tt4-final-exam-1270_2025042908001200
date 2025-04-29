namespace Backend.Models
{
    public class ExpenseItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}