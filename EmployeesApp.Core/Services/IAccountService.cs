namespace EmployeesApp.Core.Services
{
	public interface IAccountService
	{
		Task<bool> AuthenticateAsync(string? email, string? password);
		Task<bool> RegisterAsync(string? email, string? password);
	}
}
