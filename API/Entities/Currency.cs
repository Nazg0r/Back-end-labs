using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
	[PrimaryKey("Id")]
	public class Currency
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
