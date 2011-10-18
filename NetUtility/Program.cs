using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NetUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            var value = "Hello World!";
            var encryptor = new SymetricEncryption();
            var iv = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            var encrypted = encryptor.Encrypt(value, "password", iv);
            var decrypted = encryptor.Decrypt(encrypted, "password", iv);

            Console.WriteLine("Encrypted: {0}", encrypted);
            Console.WriteLine("Decrypted: {0}", decrypted);
            Console.ReadLine();
        }
    }
}
