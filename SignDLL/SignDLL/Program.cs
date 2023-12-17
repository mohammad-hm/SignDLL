using SignDLL;
using System;

namespace SignDll
{
    class Program
    {
        static void Main(string[] args)
        {
            // use for sign dll file
            //Console.WriteLine("start Signing the file");
            //Console.WriteLine("Enter the file path");
            //var filePath = Console.ReadLine();
            //Console.WriteLine("Enter the path for saving publick key & signature");
            //var sigPath = Console.ReadLine();
            //if (filePath != null && sigPath != null)
            //{
            //    SignDLL.SignDll.SignFile(filePath, sigPath); // Call the SignFile method from the SignDll class
            //}
            //Console.WriteLine("Written by mohammad hajimohammadi");

            // use for sign dll file using your own private key
            // Console.WriteLine("start Signing the file");
            // Console.WriteLine("Enter the file path");
            // var filePath = Console.ReadLine();
            // Console.WriteLine("Enter the path for saving  signature");
            // var sigPath = Console.ReadLine();
            // if (filePath != null && sigPath != null)
            // {
            //    signUsingPrivateAndPublicKey.SignFile(filePath, sigPath); // Call the SignFile method from the signUsingPrivateAndPublicKey class
            // }
            // Console.WriteLine("Written by mohammad hajimohammadi");

            // use for verifying dll file
            //Console.WriteLine("start Verifying the file");
            //Console.WriteLine("Enter the file path");
            //var filePath = Console.ReadLine();
            //Console.WriteLine("Enter the signature path");
            //var sigPath = Console.ReadLine();
            //Console.WriteLine("Enter the public key path");
            //var pubPath = Console.ReadLine();
            //if (filePath != null && sigPath != null && pubPath != null)
            //{
            //    VerifyDll.VerifyFile(filePath, sigPath, pubPath); // Call the SignFile method from the SignDll class
            //}
            //Console.WriteLine("Written by mohammad hajimohammadi");

            // use for verifying dll file using public key
            Console.WriteLine("start Verifying the file using public key");
            Console.WriteLine("Enter the file path");
            var filePath = Console.ReadLine();
            Console.WriteLine("Enter the signature path");
            var sigPath = Console.ReadLine();

            if (filePath != null && sigPath != null)
            {
                VerifyUsingPublicKey.VerifySignature(filePath, sigPath); // Call the SignFile method from the VerifyUsingPublicKey class
            }
            Console.WriteLine("Written by mohammad hajimohammadi");

            // use for creating a private and public key using RSA
            // Console.WriteLine("start creating private and public key");
            // Console.WriteLine("Enter the path for savings keys");
            // Creating.CreatingPrivateAndPublicKey(); // Call the CreatingPrivateAndPublickey method from the Creating class
            // Console.WriteLine("Written by mohammad hajimohammadi");
        }
    }
}
