using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SignDLL
{
    public class VerifyUsingPublicKey
    {
        public static bool VerifySignature(string filePath, string signatureFilePath)
        {
            try
            {
                Console.WriteLine("Task started: Signature verification");

                var publicKey = "";
                // Read the file
                byte[] data = File.ReadAllBytes(filePath);

                // Calculate the hash
                using (SHA256 alg = SHA256.Create())
                {
                    byte[] hash = alg.ComputeHash(data);

                    // Read the signature
                    byte[] signature = File.ReadAllBytes(signatureFilePath);

                    using (RSA rsa = RSA.Create())
                    {
                        // Import the public key
                        rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);

                        // create signature verifier
                        RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                        rsaDeformatter.SetHashAlgorithm(nameof(SHA256));

                        // verify the signature
                        bool isSignatureValid = rsaDeformatter.VerifySignature(hash, signature);

                        Console.WriteLine($"Signature Verification: {isSignatureValid}");

                        return isSignatureValid;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
