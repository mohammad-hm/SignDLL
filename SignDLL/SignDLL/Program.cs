using System;

namespace SignDll
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start Signing the file");
            Console.WriteLine("Enter the file path");
            var filePath = Console.ReadLine();
            Console.WriteLine("Enter the path for saving publick key & signature");
            var sigPath = Console.ReadLine();
            if (filePath != null && sigPath != null)
            {
                SignDLL.SignDll.SignFile(filePath, sigPath); // Call the SignFile method from the SignDll class
            }
            Console.WriteLine("Written by mohammad hajimohammadi");
        }
    }
}
