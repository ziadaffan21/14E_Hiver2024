using Konscious.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }
    }
}
