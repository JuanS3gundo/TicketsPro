using System;
using System.Security.Cryptography;
using System.Text;
namespace Services.Services
{
    public static class CryptographyService
    {
        private const int Iterations = 120_000;
        private const int SaltSize = 16; 
        private const int KeySize = 32;  
        private const string Prefix = "PBKDF2"; 
        public static string HashPassword(string plainTextPassword)
        {
            if (plainTextPassword == null) throw new ArgumentNullException(nameof(plainTextPassword));
            var salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider()) { rng.GetBytes(salt); }
            var key = Pbkdf2(plainTextPassword, salt, Iterations, KeySize);
            return $"{Prefix}|{Iterations}|{Convert.ToBase64String(salt)}|{Convert.ToBase64String(key)}";
        }
        public static bool ComparePassword(string plainTextPassword, string storedHash, out bool needsUpgrade)
        {
            needsUpgrade = false;
            if (string.IsNullOrWhiteSpace(storedHash)) return false;
            if (storedHash.StartsWith(Prefix + "|", StringComparison.Ordinal))
            {
                var parts = storedHash.Split('|');
                if (parts.Length != 4) return false;
                var iterations = int.Parse(parts[1]);
                var salt = Convert.FromBase64String(parts[2]);
                var expected = Convert.FromBase64String(parts[3]);
                var key = Pbkdf2(plainTextPassword, salt, iterations, expected.Length);
                var ok = FixedTimeEquals(key, expected);
                needsUpgrade = ok && (iterations < Iterations || salt.Length < SaltSize || expected.Length < KeySize);
                return ok;
            }
            if (IsLegacyMd5Hex(storedHash))
            {
                var md5Hex = Md5Hex(plainTextPassword);
                var ok = storedHash.Equals(md5Hex, StringComparison.OrdinalIgnoreCase);
                needsUpgrade = ok; 
                return ok;
            }
            return false;
        }
        public static bool ComparePassword(string plainTextPassword, string storedHash)
            => ComparePassword(plainTextPassword, storedHash, out _);
        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int keySize)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(keySize);
            }
        }
        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++) diff |= a[i] ^ b[i];
            return diff == 0;
        }
        private static bool IsLegacyMd5Hex(string s) => s.Length == 32 && IsHex(s);
        private static bool IsHex(string s)
        {
            foreach (var c in s)
            {
                bool hex = (c >= '0' && c <= '9') ||
                           (c >= 'a' && c <= 'f') ||
                           (c >= 'A' && c <= 'F');
                if (!hex) return false;
            }
            return true;
        }
        private static string Md5Hex(string input)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = md5.ComputeHash(bytes);
                var sb = new StringBuilder(hash.Length * 2);
                foreach (var b in hash) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
        public static string Sha256Base64(string input)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return Convert.ToBase64String(sha.ComputeHash(bytes));
            }
        }
    }
}
