using EmployeesApp.DataAccess;
using EmployeesApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApp.Core.Services
{
	public class AccountService : IAccountService
	{
		private readonly AppDbContext _db;

		public AccountService(AppDbContext db)
		{
			_db = db;
		}

		public async Task<bool> AuthenticateAsync(string? email, string? password)
		{
			return await _db.Users.AnyAsync(u => u.Email == email && u.Password == password);
		}

		public async Task<bool> RegisterAsync(string? email, string? password)
		{
			if (await _db.Users.AnyAsync(u => u.Email == email))
			{
				return false;
			}

			// TODO: use password hashing algorithms 
			_db.Users.Add(new User { Email = email, Password = password });
			await _db.SaveChangesAsync();

			return true;
		}
	}
}
