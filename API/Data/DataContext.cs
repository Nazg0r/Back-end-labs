using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
	public class DataContext(DbContextOptions options) : DbContext(options)
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Record> Records { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Currency> Currencies { get; set; }

	}
}
