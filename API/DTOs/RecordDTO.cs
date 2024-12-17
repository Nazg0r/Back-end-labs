namespace API.DTOs
{
	public class RecordDTO
	{
		public required int ExpensesSum { get; set; }
		public required int UserId { get; set; }
		public required int CategoryId { get; set; }
		public string? Currency { get; set; } = "USD";

	}
}
