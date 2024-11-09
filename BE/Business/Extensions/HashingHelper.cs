using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Business.Extensions;

public static class HashingHelper
{
    /// <summary>
    /// Checking login-password with storage-password
    /// </summary>
    /// <param name="storagePassword">Storage-password</param>
    /// <param name="loginPassword">Login-password: Default password input is MD5 format</param>
    /// <returns></returns>
    public static bool CheckingPassword(this string storagePassword, string loginPassword)
    {
            try
            {
                string[] arrPassword = storagePassword.Split('.');
                if (arrPassword.Length != 3)
                    return false;

                string hashingPwd = GetHashPBKDF2(loginPassword,
                    Convert.FromBase64String(arrPassword[1]),
                    Convert.ToInt32(arrPassword[0]));

                if (string.Equals(hashingPwd, arrPassword[2]))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

    /// <summary>
    /// Reference: https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-5.0
    /// </summary>
    /// <param name="password"></param>
    /// <param name="iterationCount"></param>
    /// <returns></returns>
    private static string GetHashPBKDF2(string password, byte[] salt, int iterationCount)
    {
            // Derive a 256-bit subkey (use HMAC-SHA512 with 10,000 iterations is default)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password.ToLower(),
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: iterationCount,
                numBytesRequested: 256 / 8));

            return hashed;
        }

    /// <summary>
    /// Hashing password with PBKDF2
    /// </summary>
    /// <param name="passwordMD5">Default password input is MD5 format</param>
    /// <param name="iterationCount"></param>
    /// <returns></returns>
    public static string HashingPassword(this string passwordMD5, int iterationCount = 10000)
    {
            // Generate a 128 - bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = GetHashPBKDF2(passwordMD5, salt, iterationCount);

            return string.Format($"{iterationCount}.{Convert.ToBase64String(salt)}.{hashed}"); // Format: {iterationCount}.{salt}.{hash}
        }
}