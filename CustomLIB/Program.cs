using System;
using CustomLIB.RandomLIB;

namespace CustomLIB
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = GetPassword.CreatePass();
            Console.WriteLine(result + "\nLength: " + result.Length);
            Console.ReadLine();
        }
    }
}
