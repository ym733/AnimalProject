using Microsoft.AspNetCore.Components.Forms;
using System.Security.Cryptography;

namespace Animal.Web.MediaComponents
{
	public class PasswordHasher : Core.Disposable
	{
		private const int SALTSIZE = 128 / 8;
		private const int KEYSIZE = 256 / 8;
		private const int ITERATIONS = 10000;
		private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;
		private const char DELIMETER = ';';

		public string hash(string inputPassword)
		{
			var salt = RandomNumberGenerator.GetBytes(SALTSIZE);
			var hash = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, ITERATIONS, hashAlgorithm, KEYSIZE);

			return string.Join(DELIMETER, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
		}

		public bool verify(string passwordHash, string inputPassword)
		{
			var elements = passwordHash.Split(DELIMETER);
			var salt = Convert.FromBase64String(elements[0]);
			var hash = Convert.FromBase64String(elements[1]);

			var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, ITERATIONS, hashAlgorithm, KEYSIZE);

			return CryptographicOperations.FixedTimeEquals(hash, hashInput);
		}

	}
}
