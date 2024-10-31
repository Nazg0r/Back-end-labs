namespace API.Entities
{
	public class Record
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int CategoryId { get; set; }
		public required string CreationDate { get; set; }
		public int ExpensesSum { get; set; }

		public required User User { get; set; }
		public required Category Category { get; set; }
	}
}
