using System;

namespace RSLOS
{
    static class Program
    {
        static int[][] RSLOS(int N, int[][] a, int[][] c)
        {
            int[][] at = new int[3][];
            int[][] s = new int[3][];
          
            for (int i = 0; i < 3; i++)
            {
                at[i] = (int[])a[i].Clone();
                bool f = false;
                s[i] = new int[N];
                for (int t = 0; t < N; t++)
                {
                    int r = 0;
                    for (int j = 0; j < a[i].Length; j++)
                    {
                        r ^= a[i][j] & c[i][j];
                    }

                    Array.Copy(a[i], 0, a[i], 1, a[i].Length - 1);
                    a[i][0] = r;
                    s[i][t] = a[i][^1];

                    if (Enumerable.SequenceEqual(a[i], at[i]) && !f)
                    {
                        Console.WriteLine($"{i + 1}-й период {t + 1}");
                        f = true;
                    }
                }
            }

            return s;
        }

        static int[] GeffeGenerator(int N, int[][] a, int[][] c)
        {
            int[][] s = RSLOS(N, a, c);
            int[] g = new int[s[0].Length];

            for (int i = 0; i < s[0].Length; i++)
            {
                g[i] = (s[0][i] & s[1][i]) ^ ((s[0][i] ^ 1) & s[2][i]);
            }

            return g;
        }

        static void CountDigits(int[] g, out int zeros, out int ones)
        {
            ones = g.Sum();
            zeros = g.Length - ones;
        }

        static int r(int[] g, int i)
        {
            int res = 0;
            for (int j = 0; j < g.Length - i; j++)
            {
               res += (int)Math.Pow(-1.0, g[j] ^ g[j + i]);
            }

            return res;
        }

        static void Main()
        {
            int[][] a = { new int[] { 0, 0, 1, 0, 1 },
            new int[] { 0, 1, 1, 1, 0, 0, 1 },
            new int[] { 1, 0, 0, 1, 0, 1, 0, 0 }};

            int[][] c = { new int[] { 1, 0, 1, 0, 1 },
            new int[] { 1, 1, 0, 0, 1, 1, 1 },
            new int[] { 1, 1, 0, 1, 0, 0, 1, 1 }};


            int[] g = GeffeGenerator(10000, a, c);
            int zeros, ones;
            CountDigits(g, out zeros, out ones);
            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine($"r{i} = {r(g, i)}");
            }
            Console.WriteLine($"{zeros} нулей, {ones} единиц");
        }
    }
}