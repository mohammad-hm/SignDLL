using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SignDLL
{
    public class signUsingPrivateAndPublicKey
    {
        // Replace the following private key with your actual private key
        private static string privateKey = "";
        public static void SignFile(string filePath, string signatureFilePath)
        {
            try
            {
                Console.WriteLine("Task started: Sign validation");

                SHA256 alg = SHA256.Create();

                // Read the file
                byte[] data = File.ReadAllBytes(filePath);

                // Calculate the hash
                byte[] hash = alg.ComputeHash(data);

                byte[] signedHash;

                using (RSA rsa = RSA.Create())
                {
                    // Import the private key
                    rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);

                    // create signature
                    RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                    rsaFormatter.SetHashAlgorithm(nameof(SHA256));

                    // create signature
                    signedHash = rsaFormatter.CreateSignature(hash);

                    // Save the signature to a file
                    File.WriteAllBytes($"{signatureFilePath}\\file.file", signedHash);

                    // Save the DLL file with the ".conf" extension
                    File.WriteAllBytes(filePath, data);

                    Console.WriteLine("File successfully signed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
