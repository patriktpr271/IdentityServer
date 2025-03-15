using System.Security.Cryptography;
using IdentityServer.BusinessLogic.Services.Interfaces;


namespace IdentityServer.BusinessLogic.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        private readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

        public bool Verify(string hashString, string password)
        {
            string[] parts = hashString.Split('-');
            if (parts.Length != 2)
            {
                return false; 
            }

            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] computedHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                Algorithm,
                HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, computedHash);
        }
    }
}
