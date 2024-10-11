using System;
using System.IO;
using System.Linq;

namespace Lab1
{
    public class Program
    {
        // Визначаємо клас для зберігання пари значень A і B
        public class Column
        {
            public int A { get; set; }
            public int B { get; set; }

            public Column(int a, int b)
            {
                A = a;
                B = b;
            }
        }

        // Метод для обчислення результату
        public static int CalculateGameResult(Column[] columns)
        {
            // Сортування за сумою A[i] + B[i] у порядку спадання
            var sortedColumns = columns
                .OrderByDescending(col => col.A + col.B)
                .ToArray();

            // Очки гравців
            int score1 = 0, score2 = 0;

            // Ходи гравців по черзі
            for (int i = 0; i < sortedColumns.Length; i++)
            {
                if (i % 2 == 0)
                {
                    // Хід першого гравця (вибирає A[i])
                    score1 += sortedColumns[i].A;
                }
                else
                {
                    // Хід другого гравця (вибирає B[i])
                    score2 += sortedColumns[i].B;
                }
            }

            // Вартість гри = S1 - S2
            return score1 - score2;
        }

        public static void Main()
        {
            // Читання вхідних даних
            var lines = File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab1", "INPUT.TXT"));
            int N = int.Parse(lines[0]);

            var columns = new Column[N];

            for (int i = 0; i < N; i++)
            {
                var parts = lines[i + 1].Split();
                int A = int.Parse(parts[0]);
                int B = int.Parse(parts[1]);
                columns[i] = new Column(A, B);
            }

            // Обчислення результату
            int result = CalculateGameResult(columns);

            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab1", "OUTPUT.TXT"), result.ToString());
        }
    }
}
