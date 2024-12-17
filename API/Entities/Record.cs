using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
	[PrimaryKey("Id")]
	public class Record
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int CategoryId { get; set; }
		public int? CurrencyId { get; set; } = 1;
		public required string CreationDate { get; set; }
		public int ExpensesSum { get; set; }

		[ForeignKey("UserId")]
		public virtual required User User { get; set; }
		[ForeignKey("CategoryId")]
		public virtual required Category Category { get; set; }
		[ForeignKey("CurrencyId")]
		public virtual Currency Currency { get; set; }
	}
}
