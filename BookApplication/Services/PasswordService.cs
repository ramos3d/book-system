using System.Security.Cryptography;
using System.Text;

namespace BookApplication.Services
{
	public class PasswordService
	{
		public string HashPassword(string password)
		{
			using var sha256 = SHA256.Create();
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			byte[] hash = sha256.ComputeHash(bytes);

			// Convert the hash to a hexadecimal string
			StringBuilder builder = new StringBuilder();
			foreach (byte b in hash)
			{
				builder.Append(b.ToString("x2"));
			}

			return builder.ToString();
		}

		public bool VerifyPassword(string enteredPassword, string hashedPassword)
		{
			string enteredPasswordHash = HashPassword(enteredPassword);
			return StringComparer.OrdinalIgnoreCase.Compare(enteredPasswordHash, hashedPassword) == 0;
		}
	}
}
