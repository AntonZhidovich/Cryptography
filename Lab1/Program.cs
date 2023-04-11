using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
 
namespace Hills_Cipher
{
    class Program
    {
        static readonly Matrix<double> key = DenseMatrix.OfArray(new double[,] { { 14, 16 }, { 13, 4 } });
        static readonly Matrix<double> InvKey = DenseMatrix.OfArray(new double[,] { { 13, 14 }, { 32, 29 } });
        static readonly string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        public static readonly string openStr = "ЗАНЕВСКА";
        public static readonly string encoded = "ЬЫФЫГПЛГОБУЮ";

        public static string Encode(string openStr, Matrix<double> key)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < openStr.Length - 1; i+=2)
            {
                Matrix<double> pair = DenseMatrix.OfArray(new double[,] { { alphabet.IndexOf(openStr[i]), alphabet.IndexOf(openStr[i + 1]) } });
                var res = pair.Multiply(key);
                char c1 = alphabet[(int)res[0, 0] % 33];
                char c2 = alphabet[(int)res[0,1] % 33];
                sb.Append($"{c1}{c2}");
            }

            return sb.ToString();
        }

        public static string Decode(string encoded, int k1 = 28, int k2 = 20)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var c in encoded)
            {
                sb.Append(alphabet[(alphabet.IndexOf(c) + 33 - k2) * 13 % 33]);
            }

            return sb.ToString();
        }

        public static void Main()
        {
            string encStr = Encode(openStr, key);
            Console.WriteLine($"Encoded {openStr} by hill cipher: {encStr}");
            Console.WriteLine($"Decoded {encStr} by hill cipher: {Encode(encStr, InvKey)}");
            Console.WriteLine($"Decoded {encoded} by affine cipher: {Decode(encoded)}");
        }
    }
}