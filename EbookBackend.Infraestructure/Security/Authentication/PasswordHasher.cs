using EbookBackend.Domain.Security;
using System.Security.Cryptography;


namespace EbookBackend.Infraestructure.Security.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;   
        private const int KeySize = 32;    
        private const int Iterations = 100000;

        public string Hash(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] key = pbkdf2.GetBytes(KeySize);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}.{Iterations}";
        }

        public bool Verify(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split('.');
            if (parts.Length != 3)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] key = Convert.FromBase64String(parts[1]);
            int iterations = int.Parse(parts[2]);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            byte[] keyToCheck = pbkdf2.GetBytes(KeySize);

            return keyToCheck.SequenceEqual(key);
        }
    }
}
