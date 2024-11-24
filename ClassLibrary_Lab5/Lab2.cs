using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_Lab4
{
    public class Lab2
    {
        public static void GoLab2(string inputFile, string outputFile)
        {
            try
            {
                // Перевірка наявності вхідного файлу
                if (!File.Exists(inputFile))
                {
                    Console.WriteLine("Input file doesn't exist.");
                    return;
                }

                // Зчитування вхідних даних
                string inputContent = File.ReadAllText(inputFile).Trim();

                if (string.IsNullOrEmpty(inputContent))
                {
                    Console.WriteLine("Input file is empty.");
                    return;
                }

                // Перетворення вхідного значення на ціле число
                if (!int.TryParse(inputContent, out int n))
                {
                    Console.WriteLine("Invalid input format.");
                    return;
                }

                // Виклик основної функції для обчислення
                int result = CountStrings(n);

                // Запис результату у вихідний файл
                File.WriteAllText(outputFile, result.ToString().Trim());
                Console.WriteLine($"The result is recorded in {outputFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private static int CountStrings(int n)
        {
            if (n < 1 || n > 1000)
            {
                Console.WriteLine("Input out of range.");
                return 0;
            }

            // Масиви для збереження кількості рядків
            int[] a = new int[n];
            int[] b = new int[n];

            // Початкові значення
            a[0] = b[0] = 1;

            // Заповнення масивів за рекурентною формулою
            for (int i = 1; i < n; i++)
            {
                a[i] = (a[i - 1] + b[i - 1]) % 1000000007;
                b[i] = a[i - 1];
            }

            return (a[n - 1] + b[n - 1]) % 1000000007;
        }
    }
}