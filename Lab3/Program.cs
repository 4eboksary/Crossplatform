using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {
            string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "INPUT.txt").Trim();
            string outputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "OUTPUT.txt").Trim();

            // Зчитуємо вхідні дані
            var input = File.ReadAllLines(inputPath);

            if (input.Length == 0)
            {
                Console.WriteLine("Файл порожній.");
                return;
            }

            if (!int.TryParse(input[0], out int n))
            {
                Console.WriteLine("Перший рядок не є дійсним числом.");
                return;
            }

            char[,] grid = new char[n, n];
            (int x, int y) start = (-1, -1), end = (-1, -1);

            // Заповнюємо сітку і визначаємо стартову та кінцеву точки
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    grid[i, j] = input[i + 1][j];
                    if (grid[i, j] == '@') start = (i, j);
                    if (grid[i, j] == 'X') end = (i, j);
                }
            }

            bool[,] visited = new bool[n, n];  // Мітки відвідуваних клітин
            int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } }; // Напрями руху (право, вниз, вліво, вгору)
            Queue<(int x, int y, List<(int x, int y)> path)> queue = new();
            queue.Enqueue((start.x, start.y, new List<(int x, int y)> { start }));
            visited[start.x, start.y] = true;

            // Алгоритм пошуку в ширину
            while (queue.Count > 0)
            {
                var (currentX, currentY, path) = queue.Dequeue();

                if ((currentX, currentY) == end)
                {
                    // Якщо досягли кінцевої точки, позначаємо шлях
                    foreach (var (x, y) in path)
                    {
                        if (grid[x, y] == '.')
                            grid[x, y] = '+';  // Маркуємо шлях
                    }
                    grid[start.x, start.y] = '@'; // Стартова точка
                    grid[end.x, end.y] = 'X'; // Кінцева точка

                    File.WriteAllText(outputPath, "Y\n"); // Записуємо результат в файл
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                            File.AppendAllText(outputPath, grid[i, j].ToString());
                        File.AppendAllText(outputPath, "\n");
                    }
                    return;
                }

                // Перевірка всіх можливих напрямків
                for (int i = 0; i < directions.GetLength(0); i++)
                {
                    int dx = directions[i, 0];
                    int dy = directions[i, 1];

                    int newX = currentX + dx;
                    int newY = currentY + dy;

                    // Перевірка, чи можемо йти в нову клітину
                    if (newX >= 0 && newX < n && newY >= 0 && newY < n &&
                        !visited[newX, newY] && grid[newX, newY] != 'O') // 'O' - непрохідна клітина
                    {
                        visited[newX, newY] = true;
                        var newPath = new List<(int x, int y)>(path) { (newX, newY) };
                        queue.Enqueue((newX, newY, newPath));
                    }
                }
            }

            // Якщо шлях не знайдено
            File.WriteAllText(outputPath, "N");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Сталася помилка: {ex.Message}");
        }
    }
}
