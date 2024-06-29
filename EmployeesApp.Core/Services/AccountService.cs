using EmployeesApp.DataAccess;
using EmployeesApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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
			var passwordhash = HashPassword(password);

            return await _db.Users.AnyAsync(u => u.Email == email && u.PasswordHash == passwordhash);
		}

		public async Task<bool> RegisterAsync(string? email, string? password)
		{
			if (await _db.Users.AnyAsync(u => u.Email == email))
			{
				return false;
			}

			var passwordHash = HashPassword(password);

			_db.Users.Add(new User { Email = email, PasswordHash = passwordHash });
			await _db.SaveChangesAsync();

			return true;
		}

		private static string HashPassword(string? password)
        {
			ArgumentNullException.ThrowIfNull(password);

            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
