using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Core
{
	public static class EncryptionServices
	{
		public static string Encrypt(this Object model)
		{
			Aes aes = Aes.Create();
			aes.KeySize = 256;
			aes.Mode = CipherMode.CBC;
			aes.GenerateIV();
			aes.GenerateKey();

			string strJson = JsonSerializer.Serialize(model);

			byte[] ciphertext;

			using (ICryptoTransform encryptor = aes.CreateEncryptor())
			{
				byte[] plaintextBytes = Encoding.UTF8.GetBytes(strJson);
				ciphertext = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
			}

			return Convert.ToBase64String(ciphertext);
		}
		
		public static Object Decrypt<T>(this string str)
		{
			Aes aes = Aes.Create();
			aes.KeySize = 256;
			aes.Mode = CipherMode.CBC;
			aes.GenerateIV();
			aes.GenerateKey();

			byte[] ciphertext  = Encoding.UTF8.GetBytes(str);

			string decryptedText;

			using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
			{
				byte[] decryptedBytes = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
				decryptedText = Encoding.UTF8.GetString(decryptedBytes);
			}

			return JsonSerializer.Deserialize<T>(decryptedText);
		}
	}
}
