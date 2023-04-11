using System.Numerics;
using static Lab4.BigArithm;
namespace Lab4
{

    static class Program
    {
        static BigInteger n;

        static BigInteger GeneratePrivateKey(BigInteger p, BigInteger q, BigInteger e)
        {
            BigInteger phi = (p - 1) * (q - 1);
            BigInteger x, y, d;
            if (GCD(e, phi, out x, out y) == 1)
                d = (x % phi + phi) % phi;
            else
                return -1;
            return d;
        }

        static BigInteger Encrypt(BigInteger e, BigInteger X)
        {
            return BinPow(X, e, n);
        }

        static BigInteger Decrypt(BigInteger p, BigInteger q, BigInteger e, BigInteger Y)
        {
            BigInteger d = GeneratePrivateKey(p, q, e);
            return BinPow(Y, d, n);
        }

        static void Main()
        {
            BigInteger p = BigInteger.Parse("206831527133929");
            BigInteger q = BigInteger.Parse("185339654386123");
            BigInteger e = BigInteger.Parse("30715173722744268531200609903");
            BigInteger X1 = BigInteger.Parse("28854954577191698276222341510");
            BigInteger Y2 = BigInteger.Parse("36444597938970100822252683282");
            n = p * q;

            BigInteger Y1 = Encrypt(e, X1);
            BigInteger X2 = Decrypt(p, q, e, Y2);
            Console.WriteLine($"X1 = {X1}");
            Console.WriteLine($"Y1 = {Y1}");
            Console.WriteLine($"Decrypted Y1 = {Decrypt(p, q, e, Y1)}\n");
            Console.WriteLine($"Y2 = {Y2}");
            Console.WriteLine($"X1 = {X2}");
            Console.WriteLine($"Encrypted X2 = {Encrypt(e, X2)}");
        }
    }
}