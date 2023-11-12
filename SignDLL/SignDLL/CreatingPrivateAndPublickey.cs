using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SignDLL
{
    public class Creating
    {
        public static void CreatingPrivateAndPublicKey()
        {
            try
            {
                using (RSA rsa = RSA.Create())
                {
                    // Export the public key
                    byte[] publicKeyBytes = rsa.ExportRSAPublicKey();
                    string publicKey = Convert.ToBase64String(publicKeyBytes);
                    Console.WriteLine($"Public Key:\n{publicKey}");

                    // Export the private key
                    byte[] privateKeyBytes = rsa.ExportRSAPrivateKey();
                    string privateKey = Convert.ToBase64String(privateKeyBytes);
                    Console.WriteLine($"Private Key:\n{privateKey}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
