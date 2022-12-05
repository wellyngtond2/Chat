using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Chat.Share.Helpers
{
    public static class SecurityHelper
    {
        private const int IterationCount = 10_000;

        private static RandomNumberGenerator _rng = RandomNumberGenerator.Create();

        public static string StringToHash(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var hashPassword = HashPassword(
                password: input,
                rng: _rng,
                prf: KeyDerivationPrf.HMACSHA256,
                iterCount: IterationCount,
                saltSize: 16,
                numBytesRequested: 32);

            return Convert.ToBase64String(hashPassword);
        }

        public static bool VerifyHash(string hashedPassword, string password)
        {
            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

            try
            {
                // Read header information
                KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(decodedHashedPassword, 1);
                var iterCount = (int)ReadNetworkByteOrder(decodedHashedPassword, 5);
                int saltLength = (int)ReadNetworkByteOrder(decodedHashedPassword, 9);

                // Read the salt: must be >= 128 bits
                if (saltLength < 16)
                {
                    return false;
                }

                byte[] salt = new byte[saltLength];
                Buffer.BlockCopy(decodedHashedPassword, 13, salt, 0, salt.Length);

                // Read the subkey (the rest of the payload): must be >= 128 bits
                int subKeyLength = decodedHashedPassword.Length - 13 - salt.Length;
                if (subKeyLength < 16)
                {
                    return false;
                }
                byte[] expectedSubKey = new byte[subKeyLength];
                Buffer.BlockCopy(decodedHashedPassword, 13 + salt.Length, expectedSubKey, 0, expectedSubKey.Length);

                // Hash the incoming password and verify it
                byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subKeyLength);

                return ByteArraysEqual(actualSubkey, expectedSubKey);
            }
            catch (Exception ex)
            {
                // This should never occur except in the case of a malformed payload, where
                // we might go off the end of the array. Regardless, a malformed payload
                // implies verification failed.
                return false;
            }
        }
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }

            return areSame;
        }
        private static int ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return (int)(buffer[offset] << 24 | buffer[offset + 1] << 16 | buffer[offset + 2] << 8) | buffer[offset + 3];
        }

        private static byte[] HashPassword(string password, RandomNumberGenerator rng, KeyDerivationPrf prf, int iterCount, int saltSize, int numBytesRequested)
        {
            // Produce a version 3 (see comment above) text hash.
            byte[] salt = new byte[saltSize];
            rng.GetBytes(salt);
            byte[] subKey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);

            var outputBytes = new byte[13 + salt.Length + subKey.Length];
            outputBytes[0] = (byte)1;
            WriteNetworkByteOrder(outputBytes, 1, (int)prf);
            WriteNetworkByteOrder(outputBytes, 5, (int)iterCount);
            WriteNetworkByteOrder(outputBytes, 9, (int)saltSize);
            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
            Buffer.BlockCopy(subKey, 0, outputBytes, 13 + saltSize, subKey.Length);
            return outputBytes;
        }

        private static void WriteNetworkByteOrder(byte[] buffer, int offset, int value)
        {
            buffer[offset] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)value;
        }
    }
}
