using System;
using System.IO;
namespace Lab2
{
    public class Program
    {
        public static int Count_Strings(int n)
        {
            if (n < 1 || n > 1000)
            {
                Console.WriteLine("Input out of range.");
                return 0;
            }

            int[] a = new int[n];

            int[] b = new int[n];

            a[0] = b[0] = 1;

            for (int i = 1; i < n; i++)
            {
                a[i] = a[i - 1] + b[i - 1];

                b[i] = a[i - 1];
            }
            return (a[n - 1] + b[n - 1]) % 1000000007;
        }

        public static void Main(string[] args)
        {
            string inputFile = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "INPUT.txt").Trim();

            if (!File.Exists(inputFile))
            {
                Console.WriteLine("Input file doesn't exist.");
                return;
            }

            string inputContent = File.ReadAllText(inputFile).Trim();

            if (string.IsNullOrEmpty(inputContent))
            {
                Console.WriteLine("Input file is empty.");
                return;
            }

            if (!int.TryParse(inputContent, out int N))
            {
                Console.WriteLine("Invalid input format.");
                return;
            }

            int result = Count_Strings(N);

            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "OUTPUT.txt"), result.ToString().Trim());
            Console.WriteLine($"The Result are recorded in OUTPUT.TXT");
        }
    }
}