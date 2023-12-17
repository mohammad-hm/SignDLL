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

                var publicKey = "MIIBCgKCAQEAyeCk3ijTBNKYO9ixXnZYBKKsyDaImIRsDyCY89bWrfeKHChYvwxrdGJle4LjegZiqkh2sec71RFiLcqVlmDXUa5uEPqbiciwB66jSb/sodLswylZChwWl9BRP5daJBxaAQzawBq5s2zga6lD1XMbUv/qxWST24tPy9ZQnnZ9wKiqMWHNAxc2h12N/NrDosdI62tKVQ7LZxSQU0Lu1N4rFXgrMg/0vnck4MLpeTa5qt+npXk/e3mIR6NM/gxMEEcLpyMwHdU2FmfbB7LeOJNvDuExwCw4/lBt1bFpflGvbFI8ju4BtsJLjc+mWkZ3Or6SWbzlMI20EMorv1uYhgLnZwIDAQAB";
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
                        if (isSignatureValid)
                        {
                            // Extract timestamp from data (assuming it's appended at the end)
                            int timestampSize = sizeof(long);
                            byte[] timestampBytes = new byte[timestampSize];
                            Array.Copy(data, data.Length - timestampSize, timestampBytes, 0, timestampSize);
                            long signTimeValue = BitConverter.ToInt64(timestampBytes, 0);
                            // Extract original data (excluding the timestamp)
                            int originalDataLength = data.Length - timestampBytes.Length;
                            byte[] originalData = new byte[originalDataLength];
                            Array.Copy(data, 0, originalData, 0, originalDataLength);

                            // You can use 'originalData' for further processing or save it to a file
                            File.WriteAllBytes($"{filePath}.original", originalData);
                            // Convert the sign time value to DateTime
                            DateTime signTime = DateTimeOffset.FromUnixTimeSeconds(signTimeValue).DateTime;

                            // Display the sign time
                            Console.WriteLine($"The file was signed on {signTime}");
                        }
                        else
                        {
                            Console.WriteLine("Signature is not valid.");
                        }
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
