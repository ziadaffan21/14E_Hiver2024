using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace CineQuebec.Windows.DAL
{
    public static class PasswodHasher
    {
        private const int DEGREE_OF_PARALLELISM = 16;
        private const int NUMBER_OF_ITERATIONS = 4;
        private const int MEMORY_TO_USE_IN_KB = 600000;

        public static byte[] HashPassword(string password, byte[] salt)
        {
            var argon2id = new Argon2id(Encoding.UTF8.GetBytes(password));
            argon2id.Salt = salt;
            argon2id.DegreeOfParallelism = DEGREE_OF_PARALLELISM;
            argon2id.Iterations = NUMBER_OF_ITERATIONS;
            argon2id.MemorySize = MEMORY_TO_USE_IN_KB;

            return argon2id.GetBytes(16);
        }

        public static bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }

        public static byte[] CreateSalt()
        {
            using (var generator = RandomNumberGenerator.Create())
            {
                var salt = new byte[16];
                generator.GetBytes(salt);
                return salt;
            }
        }
    }
}