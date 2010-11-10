using System;
using System.Web.Security;
using System.Security.Cryptography;

namespace Encryption
{
	/// <summary>
	/// Provides methods for creating salted password hashes and validating
	/// them against a user-entered password.
	/// </summary>
	/// <remarks>
	/// This code was written by Jeff Prosise for DevDays 2004.
	/// </remarks>
	public class SaltedHash
	{
		static public bool ValidatePassword (string password, string saltedHash) {
			// Extract hash and salt string
			string saltString = saltedHash.Substring (saltedHash.Length - 24);
			string hash1 = saltedHash.Substring (0, saltedHash.Length - 24);

			// Append the salt string to the password
			string saltedPassword = password + saltString;

			// Hash the salted password
			string hash2 = FormsAuthentication.HashPasswordForStoringInConfigFile
				(saltedPassword, "SHA1");

			// Compare the hashes
			return (hash1.CompareTo (hash2) == 0);
		}

		static public string CreateSaltedPasswordHash (string password) {
			// Generate random salt string
			RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider ();
			byte[] saltBytes = new byte[16];
			csp.GetNonZeroBytes (saltBytes);
			string saltString = Convert.ToBase64String (saltBytes);

			// Append the salt string to the password
			string saltedPassword = password + saltString;

			// Hash the salted password
			string hash = FormsAuthentication.HashPasswordForStoringInConfigFile
				(saltedPassword, "SHA1");

			// Append the salt to the hash
			string saltedHash = hash + saltString;
			return saltedHash;
		}
	}
}
