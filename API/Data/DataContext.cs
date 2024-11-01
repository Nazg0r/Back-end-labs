using API.Entities;

namespace API.Data
{
	public class DataContext
	{
		public static List<User> Users = new List<User>()
		{   new() {Id = 1, Name = "Alex"},
			new() {Id = 2, Name = "Olga"},
			new() {Id = 3, Name = "Dima"},
			new() {Id = 4, Name = "Ivan"},
			new() {Id = 5, Name = "Yana"},
			new() {Id = 6, Name = "Vika"}
		};

		public static List<Category> Categories = new List<Category>()
		{
			new() {Id = 1, Name = "Rent"},
			new() {Id = 2, Name = "Advertisement"},
			new() {Id = 3, Name = "License"},
			new() {Id = 4, Name = "Equipment"},
			new() {Id = 5, Name = "Utilities"},
			new() {Id = 6, Name = "Materials"}
		};

		public static List<Record> Records = new List<Record>()
		{
			new() 
			{
				Id = 1, 
				UserId = 3,
				CategoryId = 2,
				CreationDate = DateTime.Now.AddMicroseconds(-12523248122).ToString("yyyy-MM-dd HH:mm:ss"),
				ExpensesSum = 20, 
				User = Users[2],
				Category = Categories[1]
			},
			new()
			{
				Id = 2,
				UserId = 1,
				CategoryId = 5,
				CreationDate = DateTime.Now.AddMicroseconds(-12932664212).ToString("yyyy-MM-dd HH:mm:ss"),
				ExpensesSum = 55,
				User = Users[0],
				Category = Categories[4]
			},
			new()
			{
				Id = 3,
				UserId = 2,
				CategoryId = 6,
				CreationDate = DateTime.Now.AddMicroseconds(-18452123286).ToString("yyyy-MM-dd HH:mm:ss"),
				ExpensesSum = 34,
				User = Users[1],
				Category = Categories[5]
			},
			new()
			{
				Id = 4,
				UserId = 6,
				CategoryId = 1,
				CreationDate = DateTime.Now.AddMicroseconds(-4255120121234).ToString("yyyy-MM-dd HH:mm:ss"),
				ExpensesSum = 80,
				User = Users[5],
				Category = Categories[0]
			},
			new()
			{
				Id = 5,
				UserId = 2,
				CategoryId = 3,
				CreationDate = DateTime.Now.AddMicroseconds(-18251552612).ToString("yyyy-MM-dd HH:mm:ss"),
				ExpensesSum = 60,
				User = Users[1],
				Category = Categories[2]
			},
			new()
			{
				Id = 6,
				UserId = 5,
				CategoryId = 6,
				CreationDate = DateTime.Now.AddMicroseconds(-21124862357).ToString("yyyy-MM-dd HH:mm:ss"),
				ExpensesSum = 10,
				User = Users[4],
				Category = Categories[5]
			}
		};
	}
}
