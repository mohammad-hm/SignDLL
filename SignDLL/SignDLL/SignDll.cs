using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace SignDLL
{
    public class SignDll
    {
        private static readonly SHA256 alg = SHA256.Create();

        public static void SignFile(string filePath, string signatureFilePath)
        {
            try
            {
                Console.WriteLine("Task started: Sign validation");

                // Read the file
                byte[] data = File.ReadAllBytes(filePath);

                // Calculate the hash
                byte[] hash = alg.ComputeHash(data);

                byte[] publicOnlyKey;

                // Generate a signature
                using (RSA rsa = RSA.Create())
                {
                    // Get the public key parameters
                    publicOnlyKey = rsa.ExportRSAPublicKey();

                    // Save the public key to a file
                    File.WriteAllBytes($"{signatureFilePath}\\publick.pubkey", publicOnlyKey);

                    // create signature
                    RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                    rsaFormatter.SetHashAlgorithm(nameof(SHA256));

                    byte[] signedHash = rsaFormatter.CreateSignature(hash);

                    // Save the signature to a file
                    File.WriteAllBytes($"{signatureFilePath}\\signature.sig", signedHash);

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
