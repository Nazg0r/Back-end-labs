using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class RecordController : BaseApiController
	{
		[HttpGet("{id}")]
		public IActionResult GetRecord(int id)
		{
			Record? record = DataContext.Records.FirstOrDefault(rec => rec.Id == id);
			if (record == null)
			{
				return NotFound("Record is not found in system");
			}

			return Ok(new
			{
				Id = record.Id,
				UserName = record.User.Name,
				CategoryName = record.Category.Name,
				CreationDate = record.CreationDate,
				ExpensesSum = record.ExpensesSum
			});
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteRecord(int id)
		{
			Record? record = DataContext.Records.FirstOrDefault(rec => rec.Id == id);
			if (record == null)
			{
				return NotFound("Record is not found in system");
			}
			DataContext.Records.Remove(record);

			return Ok(new
			{
				Id = record.Id,
				UserName = record.User.Name,
				CategoryName = record.Category.Name,
				CreationDate = record.CreationDate,
				ExpensesSum = record.ExpensesSum
			});
		}

		[HttpPost]
		public IActionResult AddRecord(RecordDTO record)
		{

			if (!DataContext.Users.Any(usr => usr.Id == record.UserId))
			{
				return NotFound("User with such id is not found in system");
			}

			if (!DataContext.Categories.Any(ctgry => ctgry.Id == record.CategoryId))
			{
				return NotFound("Category with such id is not found in system");
			}

			Record newRecord = new()
			{
				Id = DataContext.Records.Count + 1,
				UserId = record.UserId,
				CategoryId = record.CategoryId,
				CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
				ExpensesSum = record.ExpensesSum,
				User = DataContext.Users[record.UserId - 1],
				Category = DataContext.Categories[record.CategoryId - 1]
			};

			DataContext.Records.Add(newRecord);

			return Ok(new
			{
				Id = newRecord.Id,
				UserName = newRecord.User.Name,
				CategoryName = newRecord.Category.Name,
				CreationDate = newRecord.CreationDate,
				ExpensesSum = newRecord.ExpensesSum
			});
		}

		[HttpGet]
		public IActionResult GetRecords(RecordParamsDTO recordParams)
		{
			if (recordParams.UserId == 0 && recordParams.CategoryId == 0)
			{
				return BadRequest("None of the parameters are set");
			}

			IEnumerable<Record>? records;
			if (recordParams.UserId != 0 && recordParams.CategoryId == 0)
			{
				records = DataContext.Records.Where(rec => rec.User.Id == recordParams.UserId);
			}
			else if (recordParams.UserId == 0 && recordParams.CategoryId != 0)
			{
				records = DataContext.Records.Where(rec => rec.Category.Id == recordParams.CategoryId);
			}
			else
			{
				records = DataContext.Records.Where(rec => 
					rec.User.Id == recordParams.UserId &&
					rec.Category.Id == recordParams.CategoryId);
			}

			if (!records.Any())
			{
				return NotFound("No records were found according to the specified parameters");
			}

			return Ok(records.Select(rec => new 
			{
				Id = rec.Id,
				UserName = rec.User.Name,
				CategoryName = rec.Category.Name,
				CreationDate = rec.CreationDate,
				ExpensesSum = rec.ExpensesSum
			}));
		}
	}
}
