﻿using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[Authorize]
	public class RecordController(DataContext dbContext) : BaseApiController
	{
		[HttpGet("{id}")]
		public async Task<IActionResult> GetRecord(int id)
		{
			Record? record = await dbContext.Records.FindAsync(id);
			if (record is null)
				return NotFound("Record is not found in system");

			return Ok(new
			{
				Id = record.Id,
				UserName = record.User.Name,
				CategoryName = record.Category.Name,
				CreationDate = record.CreationDate,
				ExpensesSum = record.ExpensesSum,
				Currency = record.Currency.Name
			});
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteRecord(int id)
		{
			Record? record = await dbContext.Records.FindAsync(id);
			if (record == null)
				return NotFound("Record is not found in system");

			var response = new
			{
				Id = record.Id,
				UserName = record.User.Name,
				CategoryName = record.Category.Name,
				CreationDate = record.CreationDate,
				ExpensesSum = record.ExpensesSum,
				Currency = record.Currency.Name
			};
			
			dbContext.Records.Remove(record);
			await dbContext.SaveChangesAsync();

			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> AddRecord(RecordDTO record)
		{
			User? user = await dbContext.Users.FirstOrDefaultAsync(usr => usr.Id == record.UserId);
			Category? category = await dbContext.Categories.FirstOrDefaultAsync(ctgry => ctgry.Id == record.CategoryId);

			if (user is null)
				return NotFound("User with such id is not found in system");

			if (category is null)
				return NotFound("Category with such id is not found in system");

			Currency? currency = await dbContext.Currencies.FirstOrDefaultAsync(c => c.Name == record.Currency);

			if (currency is null)
				return BadRequest("No such currency in our system. Try \"USD\", \"EUR\", \"UAH\"");

			Record newRecord = new()
			{
				UserId = record.UserId,
				CategoryId = record.CategoryId,
				CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
				ExpensesSum = record.ExpensesSum,
				User = user,
				Category = category,
				CurrencyId = currency.Id,
				Currency = currency
			};

			dbContext.Records.Add(newRecord);
			await dbContext.SaveChangesAsync();

			return Ok(new
			{
				Id = newRecord.Id,
				UserName = newRecord.User.Name,
				CategoryName = newRecord.Category.Name,
				CreationDate = newRecord.CreationDate,
				ExpensesSum = newRecord.ExpensesSum,
				Currency = newRecord.Currency.Name
			});
		}

		[HttpGet]
		public async Task<IActionResult> GetRecords(RecordParamsDTO recordParams)
		{
			IEnumerable<Record>? records = null;
			if (recordParams.UserId == 0 && recordParams.CategoryId == 0)
				records = await dbContext.Records.ToListAsync();

			IQueryable<Record> query = dbContext.Records;

			if (recordParams.UserId != 0)
				query = query.Where(rec => rec.User.Id == recordParams.UserId);
			if (recordParams.CategoryId != 0)
				query = query.Where(rec => rec.Category.Id == recordParams.CategoryId);

			if (records is null)
				records = query.ToList();

			if (!records.Any())
				return NotFound("No records were found according to the specified parameters");
			
			var result = records.Select(rec => new
			{
				Id = rec.Id,
				UserName = rec.User?.Name,
				CategoryName = rec.Category?.Name,
				CreationDate = rec.CreationDate,
				ExpensesSum = rec.ExpensesSum,
				Currency = rec.Currency.Name
			});

			return Ok(result);
		}
	}
}
