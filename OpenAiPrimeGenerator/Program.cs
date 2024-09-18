using System;
using System.Numerics;
using OpenAIRandomPrime;

namespace OpenAiPrimeGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of bits for the prime number: ");
            string inputNumBits = Console.ReadLine();

            if (int.TryParse(inputNumBits, out int numBits))
            {
                BigInteger p = RandomPrime.GeneratePrime(numBits);
                Console.WriteLine("Generated prime:\n" + p);
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }
}