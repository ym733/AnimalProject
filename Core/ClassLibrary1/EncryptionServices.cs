﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Core
{
	public static class EncryptionServices
	{

		public static byte[] genIV = null;
		public static byte[] genKey = null;

		public static string Encrypt<T>(this T model)
		{
			using (Aes aes = Aes.Create())
			{
				aes.GenerateIV();
				aes.GenerateKey();

				genIV = aes.IV;
				genKey = aes.Key;

				string str = null;

				if (!typeof(T).IsPrimitive)
				{
					str = JsonSerializer.Serialize(model);
				}
				else
				{
					str = model.ToString();
				}

				using (ICryptoTransform encryptor = aes.CreateEncryptor())
				{
					byte[] plaintextBytes = Encoding.UTF8.GetBytes(str);
					byte[] ciphertext = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
					return Convert.ToBase64String(ciphertext);
				}
			}
		}

		public static T Decrypt<T>(this string str, byte[] key, byte[] iv)
		{
			using (Aes aes = Aes.Create())
			{
				aes.Key = key;
				aes.IV = iv;

				byte[] ciphertext = Convert.FromBase64String(str);

				using (ICryptoTransform decryptor = aes.CreateDecryptor())
				{
					byte[] decryptedBytes = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
					string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

					if (!typeof(T).IsPrimitive)
					{
						return JsonSerializer.Deserialize<T>(decryptedText);
					}
					else
					{
						return (T)Convert.ChangeType(decryptedText, typeof(T));
					}
				}
			}
		}
	}

}
