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

                // read public key 48130
                // byte[] pubKey = File.ReadAllBytes(publicKeyPath);

                //File.WriteAllBytes("C:\\Users\\mhm\\Documents\\Git\\Vishkadeh-Service\\VishkavUpdate\\bin\\Debug\\net6.0\\real\\real1\\pub.pub", pubKey);
                // Read the public key
                var pubKey = Convert.FromBase64String("MIIBCgKCAQEAk+OPWKM+7yXmeZAEg28FcB4dsGozR2SnOr17IyKoDwiLdVMw8DIwB0bYsugoT+oidVnODZQ0GtfxuLIN0jujo4KbNtofAnNhcQ8LL9todzBs3uG+u1OMAHw/UratmlSo8/dPM1yj9bfl8sGgRhIGi/542TYgFlp12RpGe63HwvQRJj1+7TaTK0AKHKB8J774VeDeQsh7V05iG8uJjocVIFOcT48E7zcXlEXmqG2hp+H9JMwBO54ZejncFa8DpftBHkwcBpjwp7/LisGxmiph5RvUXmE2EnBDcWX/FJEMhuqOvSpume+N8zWg3W1AuTGLSGiDvsI7T9qB87B3egAFpQIDAQAB");
                // read signature file
                byte[] signedHash = File.ReadAllBytes(signatureFilePath);

               
                // Write the contents to the destination file
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
