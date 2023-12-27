using System.Security.Cryptography;
using Eventy.Service.Domain.Hash.Interfaces;

namespace Eventy.Service.Domain.Hash
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16; // 128 / 8 
        private const int KeySize = 32; // 256 / 8
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private static char Delimiter = '|';

        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);

            return string.Join(Delimiter, Convert.ToBase64String(salt),  Convert.ToBase64String(hash));
        }

        public bool Verify(string password, string passwordHash)
        {
            var elements = passwordHash.Split(Delimiter);

            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);

            var newHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);

            return CryptographicOperations.FixedTimeEquals(hash, newHash);
        }
    }
}
