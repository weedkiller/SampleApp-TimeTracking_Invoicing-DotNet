using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace TimeTracking.Models
{
    public static class Utility
    {
        public static string Decrypt(string encryptText, string securityKey)
        {
            TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();
            TripleDes.Key = TruncateHash(securityKey, TripleDes.KeySize / 8);
            TripleDes.IV = TruncateHash(string.Empty, TripleDes.BlockSize / 8);

            // Convert the encrypted text string to a byte array.
            byte[] encryptedBytes = Convert.FromBase64String(encryptText);

            // Create the stream.
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Create the decoder to write to the stream.
            CryptoStream decStream = new CryptoStream(ms, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            decStream.FlushFinalBlock();

            // Convert the plaintext stream to a string.
            return System.Text.Encoding.Unicode.GetString(ms.ToArray());
        }
     
        private static  byte[] TruncateHash(string key, int length)
        {

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            // Hash the key.
            byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
            byte[] hash = sha1.ComputeHash(keyBytes);

            // Truncate or pad the hash.
            Array.Resize(ref hash, length);
            return hash;
        }
        public static string Encrypt(string plainText, string securityKey)
        {
            TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();
            TripleDes.Key = TruncateHash(securityKey, TripleDes.KeySize / 8);
            TripleDes.IV = TruncateHash(string.Empty, TripleDes.BlockSize / 8);

            // Convert the plaintext string to a byte array.
            byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(plainText);

            // Create the stream.
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Create the encoder to write to the stream.
            CryptoStream encStream = new CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            encStream.FlushFinalBlock();

            // Convert the encrypted stream to a printable string.
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}