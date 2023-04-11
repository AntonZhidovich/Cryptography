using System.Numerics;
namespace Lab4
{
    static class BigArithm
    {
        public static BigInteger GCD(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }

            BigInteger g = GCD(b, a % b, out y, out x);
            y -= (a / b) * x;
            return g;
        }

        public static BigInteger BinPow(BigInteger a, BigInteger n, BigInteger m)
        {
            BigInteger res = 1;
            while (n > 0)
            {
                if (n % 2 == 1)
                    res = (res * a) % m;
                a = (a * a) % m;
                n >>= 1;
            }
            return res;
        }
    }
}
