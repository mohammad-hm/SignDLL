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
        private static string privateKey = "MIIEowIBAAKCAQEAyeCk3ijTBNKYO9ixXnZYBKKsyDaImIRsDyCY89bWrfeKHChYvwxrdGJle4LjegZiqkh2sec71RFiLcqVlmDXUa5uEPqbiciwB66jSb/sodLswylZChwWl9BRP5daJBxaAQzawBq5s2zga6lD1XMbUv/qxWST24tPy9ZQnnZ9wKiqMWHNAxc2h12N/NrDosdI62tKVQ7LZxSQU0Lu1N4rFXgrMg/0vnck4MLpeTa5qt+npXk/e3mIR6NM/gxMEEcLpyMwHdU2FmfbB7LeOJNvDuExwCw4/lBt1bFpflGvbFI8ju4BtsJLjc+mWkZ3Or6SWbzlMI20EMorv1uYhgLnZwIDAQABAoIBADTsD7xvrlYS0TbLKUBOvOdGMA5ygV+eQ2e5z28I8JF9lbao+w7mdhr2Go2E9BZOqznhskAUEtLZ8kd31rwr2fZ/SDpqQDEgK8lvxj20sxd/IHNPGRj14RReV5ZYmphh2FRzJrt6phj7319J6c8AlHavQUALDFnfDQWuQscv3+qLtY9XQgPgdQhEuEUyWDGHfC//lX4HnXNusm0EBA56cSXTAITYdH83DKniJ7CDY8YNn2BKFSxIgndInRMkJpEBd1BwybRh/9ScO7aQZ4FxpxmRC2kj/j38GNyYEx2QLXls3wgi7OMiD6LERLPshIpJfzD9vY73JVXpHvmtcSgV+HkCgYEA567G+37AgmZ/JXMqCdHFv+23CgenDr9tRVMaD9BVK+WwNlLVXv8ZGMzgIrD2AebfKYcLl2qo83BfhaBZNCLWGkGnPZ+8E6J87Hr/C1uhA7epQj09gtZTd59S1xKDGTayYL9vefMK/IaigALTTX8zrXIz/16qZmf4UJOyMRmscW8CgYEA3xEDPJNrJCPxJxphi622Ez8i1/tBrHE790kqhEAurMRzITI19AVsAsfbsK6fYI2dwTmfYWL5Wm0U8Nh0do0wr84o0m5tWYQyF6Qd88fh8EX0OFfIHuaAmJivQ1s9lpw5lNqLMCP9XCSkzVouSvx4r5kAiLXXhwLVwwUIetaZfYkCgYBT0JHh1+gTBLuIs9IIfWA53+iw8zXHiw8f3kF2aRr254BJFsxkMotEwpFvW2+UHo6rOlTCMW7vqr6T2/+JcfTcyNWU4J2syMLC1nhABqUIcEAGW5tOIvVNOFFCx0qwK256u6a7imZ2Fsrg3qTMy3DJlaAkCX5Bz+kwVU5Inw4YdwKBgQDb0qAl6gEDtIfJyS2nwKBVhzqHLYwiq9Q0dV34xmc4yr/KTvjsaQtwEhRloTBerjBwYqC2EBs7CZRBZI4g5jK8jaWc+kW4mZXjxk/eFXSKm+V4QMlt6imAN7c/4YJoEbyM/HF+F3lufI4L8Hr+Wp90LItiLhEfc6PEdAlPRHwuQQKBgEcem+WMTZIsMjCjNevXLPVhBwLiXFUnScCqaoBY0TmjSndtCSRbAXmbfWw/puPvSaiC3nXheE5Yl+VonjDmW4FclbkTirB0ccgNAsx08CsRPP3NGYlIMMVsRM6cRMnPyM6UAV1x7yRBB1WPMbCqLZei97JBODSvtzMzd++APffY";
           public static void SignFile(string filePath, string signatureFilePath)
        {
            try
            {
                Console.WriteLine("Task started: Sign validation");

                SHA256 alg = SHA256.Create();

                // Read the file
                byte[] data = File.ReadAllBytes(filePath);

                // Include the current timestamp in the data
                long signTimeValue = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
                byte[] timestampBytes = BitConverter.GetBytes(signTimeValue);

                // Concatenate data and timestamp
                byte[] dataWithTimestamp = new byte[data.Length + timestampBytes.Length];
                Array.Copy(data, 0, dataWithTimestamp, 0, data.Length);
                Array.Copy(timestampBytes, 0, dataWithTimestamp, data.Length, timestampBytes.Length);


                // Calculate the hash
                byte[] hash = alg.ComputeHash(dataWithTimestamp);

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
                    File.WriteAllBytes($"{signatureFilePath}/file.file", signedHash);

                    // Save the dataWithTimestamp
                    File.WriteAllBytes($"{signatureFilePath}/data.data", dataWithTimestamp);

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
