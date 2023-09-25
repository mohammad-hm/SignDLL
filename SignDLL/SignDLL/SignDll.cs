using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace SignDLL
{
    public class SignDll
    {

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

                byte[] publicOnlyKey;

                byte[] signedHash;

                // Generate a signature
                using (RSA rsa = RSA.Create())
                {
                    // Get the public key parameters
                    publicOnlyKey = rsa.ExportRSAPublicKey();

                    // create signature
                    RSAPKCS1SignatureFormatter rsaFormatter = new (rsa);
                    rsaFormatter.SetHashAlgorithm(nameof(SHA256));

                    // create signature
                    signedHash = rsaFormatter.CreateSignature(hash);

                    // Save the public key to a file
                    File.WriteAllBytes($"{signatureFilePath}\\PublicKey.pubkey", publicOnlyKey);


                    // Save the signature to a file
                    File.WriteAllBytes($"{signatureFilePath}\\hajisig.hajisig", signedHash);

                    // Save the DLL file with the ".conf" extension
                    File.WriteAllBytes("C:\\Users\\mhm\\Documents\\GitHub\\SignDLL\\SignDLL\\SignDLL\\bin\\Debug\\net6.0\\haji.hajiconf", data);

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
