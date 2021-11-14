using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Enum;
using Core.Interfaces;

namespace Infrastructure.Data.Implementations
{
    public class SecurityService : ISecurityService
    {
        public string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public string GetHashMini(string text, HashType hashType__1)
        {
            HashAlgorithm _algorithm;
            switch (hashType__1)
            {
                case HashType.MD5:
                    _algorithm = MD5.Create();
                    break;

                case HashType.SHA1:
                    _algorithm = SHA1.Create();
                    break;

                case HashType.SHA256:
                    _algorithm = SHA256.Create();
                    break;

                case HashType.SHA512:
                    _algorithm = SHA512.Create();
                    break;

                default:
                    throw new ArgumentException("Invalid hash type", "hashType");
            }
            // UTF8Encoding encoder = new UTF8Encoding();
            // byte[] bytes = Encoding.Unicode.GetBytes(text);
            byte[] hash = _algorithm.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            string hashString = string.Empty;
            foreach (byte x in hash)
                hashString += String.Format("{0:x2}", x);
            return hashString.ToUpper();
        }

        public string GetHashWithBase64Regular(string text, HashType hashType__1)
        {
            HashAlgorithm _algorithm;
            switch (hashType__1)
            {
                case HashType.MD5:
                    _algorithm = MD5.Create();
                    break;
                case HashType.SHA1:
                    _algorithm = SHA1.Create();
                    break;
                case HashType.SHA256:
                    _algorithm = SHA256.Create();
                    break;
                case HashType.SHA512:
                    _algorithm = SHA512.Create();
                    break;
                default:
                    throw new ArgumentException("Invalid hash type", "hashType");
            }
            // UTF8Encoding encoder = new UTF8Encoding();
            // byte[] bytes = Encoding.Unicode.GetBytes(text);
            byte[] hash = _algorithm.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            string hashString = Convert.ToBase64String(hash);

            return hashString;
        }

        public string GetUniqueKeyRNG(int KeyLength, NumberType Type = NumberType.ALPHANUMERIC)
        {
            string a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            switch (Type)
            {
                case NumberType.ALPHABET:
                    {
                        a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        break;
                    }
                case NumberType.NUMERIC:
                    {
                        a = "1234567890";
                        break;
                    }
                case NumberType.ALPHANUMERIC:
                    {
                        a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                        break;
                    }
            }

            char[] chars = new char[] { Convert.ToChar(a.Length - 1) };
            byte[] data = new byte[] { Convert.ToByte(KeyLength - 1) };
            chars = a.ToCharArray();
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(KeyLength);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }

            return result.ToString();
        }

        private string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        public string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        public string GetCode(string prefix)
        {
            return prefix + "-" + GenerateToken(5);
        }
        public string GenerateToken(int size)
        {
            char[] chars = new char[62];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[size];
                crypto.GetNonZeroBytes(data);
            }

            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}