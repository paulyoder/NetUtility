using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace NetUtility
{
    public class SymetricEncryption
    {
        public string Encrypt(string value, string password, byte[] iv)
        {
            var aes = new RijndaelManaged();
            var passwordDerived = new Rfc2898DeriveBytes(password, Encoding.ASCII.GetBytes("SaltyThePig"), 2);
            aes.Key = passwordDerived.GetBytes(256 / 8);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            var encryptor = aes.CreateEncryptor();
            using (var mStream = new MemoryStream())
            using (var cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write))
            using (var sWriter = new StreamWriter(cStream))
            {
                sWriter.Write(value);
                sWriter.Close();
                return Convert.ToBase64String(mStream.ToArray());
            }
        }
        
        public string Decrypt(string cypher, string password, byte[] iv)
        {
            var aes = new RijndaelManaged();
            var passwordDerived = new Rfc2898DeriveBytes(password, Encoding.ASCII.GetBytes("SaltyThePig"), 2);
            aes.Key = passwordDerived.GetBytes(256 / 8);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            var decryptor = aes.CreateDecryptor();
            using (var mStream = new MemoryStream(Convert.FromBase64String(cypher)))
            using (var cStream = new CryptoStream(mStream, decryptor, CryptoStreamMode.Read))
            using (var sReader = new StreamReader(cStream))
            {
                return sReader.ReadToEnd();
            }
        }
    }
}
