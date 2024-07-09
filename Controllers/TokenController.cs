using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using SaleAanalyticsApp.Models;
using SaleAanalyticsApp.Services;
using System.Security.Cryptography;
using System.Text;

namespace SaleAanalyticsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private readonly string encryptionKey;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public TokenController(IConfiguration configuration, TokenService tokenService)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            encryptionKey = GenerateRandomKey();
        }

        /// <summary>
        /// Generate JWT Token based authentication
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Token</returns>
        [HttpPost]
        public IActionResult GenerateToken([FromBody] JwtTokenRequest request)
        {
            try
            {
                if (request == null || (request.secretKey == "string") || string.IsNullOrEmpty(request.secretKey))
                {
                    logger.Error("Invalid request. Secret key is required.");
                    return BadRequest("Secret key is required.");
                }
                var secretKeyFromConfig = _configuration["JwtSettings:secretKey"];

                // Encrypt the secret key before storing it securely
                var encryptedSecretKey = EncryptString(secretKeyFromConfig, encryptionKey);

                // Decrypt the encrypted secret key before using it
                var decryptedSecretKey = DecryptString(encryptedSecretKey, encryptionKey);

                // Validate secret key
                if (request.secretKey != decryptedSecretKey)
                {
                    logger.Warn("Unauthorized access attempt with invalid secret key.");
                    return Unauthorized("Invalid secret key.");
                }

                // Generate and return token
                var token = _tokenService.GenerateToken(decryptedSecretKey);
                logger.Info("Token generated successfully.");

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        #region Encryption and Decryption methods here

        // Generated a key randomly
        private static string GenerateRandomKey()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 32) // 32 is the length of the key
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Encrypt a string using AES
        private string EncryptString(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[16]; // Use a static IV for simplicity, don't use this in production
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        // Decrypt a string using AES
        private string DecryptString(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[16]; // Use a static IV for simplicity, don't use this in production
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        #endregion
    }
}
