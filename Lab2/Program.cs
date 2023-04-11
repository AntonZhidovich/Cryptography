using System.Text;
namespace Lab2
{
    static class Program
    {
        static string name = "Антон", surname = "Жидович";
        static int N = 7;

        static string SPChifer(string x, string key, int[,] S)
        {  
            string[] k = { key[..8], key[5..] + key[0], key[^2..] + key[..6] };
            for(int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{i + 1}-я итерация. Вход X = {x}");
                Console.WriteLine($"Ключ k = {k[i]}");
                string T = xor(x, k[i]);
                string T1 = T[0..4], T2 = T[4..];
                //Console.WriteLine($"(T1 | T2) = ({T1} | {T2})");
                string N1 = Convert.ToString(S[0, Convert.ToInt32(T1, 2)], 2).PadLeft(4, '0');
                string N2 = Convert.ToString(S[1, Convert.ToInt32(T2, 2)], 2).PadLeft(4, '0');
                //Console.WriteLine($"(N1 | N2) = ({N1} | {N2})");
                int Nnum = Convert.ToInt32(N1 + N2, 2);
                string P = Convert.ToString((Nnum << 3 | Nnum >> 8 - 3) & 255, 2).PadLeft(8, '0');
                Console.WriteLine($"Выход Y = {P}\n");
                x = P;
            }

            return x;
        }

        static void Main()
        {
            string x = Convert.ToString(7 * N, 2).PadLeft(8, '0');
            string k = Convert.ToString(4096 - 11 * name.Length * surname.Length, 2).PadLeft(12, '0');
            Console.WriteLine($"12-битовый ключ: {k}");
            int[,] S = { {3, 7, 0xE, 9, 8, 0xA, 0xF, 0, 5, 2, 6, 0xC, 0xB, 4, 0xD, 1},
                {0xB, 5, 1,  9, 8, 0xD, 0xF, 0, 0xE, 4, 2, 3, 0xC, 7, 0xA, 6} };
            Console.WriteLine($"Зашифрованный текст: {SPChifer(x, k, S)}");
            Console.WriteLine($"\nЗаменяем первый бит на противоположный");
            x = x[0] == '1' ? "0" : "1" + x[1..];
            Console.WriteLine($"Зашифрованный текст: {SPChifer(x, k, S)}");
        }

        static string xor(string l, string r)
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < l.Length; i++)
            {
                int a = int.Parse(l[i].ToString());
                int b = int.Parse(r[i].ToString());
                res.Append((a ^ b % 2).ToString());
            }

            return res.ToString();
        }
    }
}