using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
	[PrimaryKey("Id")]
	[Table("records")]
	public class Record
	{
		[Column("id")]
		public int Id { get; set; }
		[Column("user_id")]
		public int UserId { get; set; }
		[Column("category_id")]
		public int CategoryId { get; set; }
		[Column("currency_id")]
		public int? CurrencyId { get; set; } = 1;
		[Column("creation_date")]
		public required string CreationDate { get; set; }
		[Column("expenses_sum")]
		public int ExpensesSum { get; set; }

		[ForeignKey("UserId")]
		public virtual required User User { get; set; }
		[ForeignKey("CategoryId")]
		public virtual required Category Category { get; set; }
		[ForeignKey("CurrencyId")]
		public virtual Currency Currency { get; set; }
	}
}
