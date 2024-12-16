using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
	[PrimaryKey("Id")]
	public class Record
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int CategoryId { get; set; }
		public required string CreationDate { get; set; }
		public int ExpensesSum { get; set; }

		public virtual required User User { get; set; }
		public virtual required Category Category { get; set; }
	}
}
