using System.Numerics;

namespace OpenAIRandomPrime
{
    public class RandomPrime
    {
        public static BigInteger GeneratePrime(int numBits)
        {
            BigInteger p;

            do
            {
                p = RandomNumber(numBits);
            }
            while (!IsProbablePrime(p));

            return p;
        }

        public static bool IsProbablePrime(BigInteger n)
        {
            if (n < 2)
            {
                return false;
            }

            if (n == 2 || n == 3)
            {
                return true;
            }

            if ((n & 1) == 0)
            {
                return false;
            }

            // Schreibe n - 1 als 2^s * d wobei d ungerade ist
            BigInteger d = n - 1;
            int s = 0;

            
            while ((d & 1) == 0) // überprüft, ob d gerade ist, indem das niedrigstwertige Bit von d geprüft wird (Bitweises UND mit 1)
            {
                s++;
                d >>= 1; // verschiebt d um eine Zweierpotenz nach recht, was einer Division durch 2 gleich kommt
            }

            // Test witness
            const int witness = 28;
            BigInteger x = BigInteger.ModPow(witness, d, n); // berechnet x = witness^d mod  n effizient mit dem "Modular Exponentiation"-Algorithmus

            // Wenn x = 1, besteht eine hohe Wahrscheinlichkeit, dass n eine Primzahl ist (nach Fermats kleinem Satz)
            // Wenn x = n − 1, ist n ebenfalls wahrscheinlich eine Primzahl, weil es darauf hindeutet, dass n den Miller-Rabin-Test bestanden hat
            if (x == 1 || x == n - 1)
            {
                return true;
            }

            // Wiederholter Quadrat-Test
            for (int i = 0; i < s - 1; i++)
            {
                x = BigInteger.ModPow(x, 2, n); // berechnet x^2 mod n

                if (x == 1) // n ist sicher keine Primzahl (es liegt ein Nichttriviales Quadratwurzel von 1 mod n vor, was bedeutet, dass n eine zusammengesetzte Zahl ist)
                {
                    return false;
                }

                if (x == n - 1)
                {
                    return true;
                }
            }

            return false;
        }

        public static BigInteger RandomNumber(int bits)
        {
            var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            var buffer = new byte[(bits + 7) / 8];
            rng.GetBytes(buffer);
            buffer[^1] |= (byte)(0x01 << (bits % 8));
            BigInteger prime = new(buffer);
            return prime;
        }
    }
}
