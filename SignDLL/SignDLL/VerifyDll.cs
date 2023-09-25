using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SignDLL
{
    public class VerifyDll
    {


        public static void VerifyFile(string filePath, string signatureFilePath, string publicKeyPath)
        {
            try
            {
                Console.WriteLine("Task started: Verify validation");

                // Read the file
                byte[] data = File.ReadAllBytes(filePath);

                // read public key 
                byte[] pubKey = File.ReadAllBytes(publicKeyPath);

                // read signature file
                byte[] signedHash = File.ReadAllBytes(signatureFilePath);

                SHA256 alg = SHA256.Create();

                // Calculate the hash
                byte[] hash = alg.ComputeHash(data);

                // Verify signature
                using (RSA rsa = RSA.Create())
                {
                    rsa.ImportRSAPublicKey(pubKey, out _);
                    RSAPKCS1SignatureDeformatter rsaDeformatter = new(rsa);
                    rsaDeformatter.SetHashAlgorithm(nameof(SHA256));

                    if (rsaDeformatter.VerifySignature(hash, signedHash))
                    {
                        Console.WriteLine("The signature is valid.");
                    }
                    else
                    {
                        Console.WriteLine("The signature is not valid.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


    }
}
