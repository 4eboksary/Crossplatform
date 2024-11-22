using System;
using System.Collections.Generic;
using System.IO;

namespace ClassLibrary_Lab4
{
    public class Lab3
    {
        public static void GoLab3(string inputFile, string outputFile)
        {
            try
            {
                // Зчитуємо вхідні дані
                string[] input = ReadInputData(inputFile);

                // Перевіряємо розмірність сітки
                int n = ValidateGridSize(input[0]);

                char[,] grid = new char[n, n];
                (int x, int y) start = (-1, -1), end = (-1, -1);

                // Заповнюємо сітку і визначаємо стартову та кінцеву точки
                InitializeGrid(input, n, ref grid, ref start, ref end);

                // Виконуємо пошук шляху
                bool pathFound = FindPath(grid, start, end, outputFile);

                if (!pathFound)
                {
                    File.WriteAllText(outputFile, "N");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сталася помилка: {ex.Message}");
            }
        }

        private static string[] ReadInputData(string inputFile)
        {
            if (!File.Exists(inputFile))
            {
                throw new FileNotFoundException($"Файл не знайдено: {inputFile}");
            }

            string[] input = File.ReadAllLines(inputFile);
            if (input.Length == 0)
            {
                throw new ArgumentException("Файл порожній.");
            }

            return input;
        }

        private static int ValidateGridSize(string firstLine)
        {
            if (!int.TryParse(firstLine, out int n) || n <= 0)
            {
                throw new ArgumentException("Невірний формат розмірності сітки.");
            }

            return n;
        }

        private static void InitializeGrid(string[] input, int n, ref char[,] grid, ref (int x, int y) start, ref (int x, int y) end)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    grid[i, j] = input[i + 1][j];
                    if (grid[i, j] == '@') start = (i, j);
                    if (grid[i, j] == 'X') end = (i, j);
                }
            }

            if (start == (-1, -1) || end == (-1, -1))
            {
                throw new ArgumentException("Стартова або кінцева точка відсутня.");
            }
        }

        private static bool FindPath(char[,] grid, (int x, int y) start, (int x, int y) end, string outputFile)
        {
            int n = grid.GetLength(0);
            bool[,] visited = new bool[n, n];
            int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
            Queue<(int x, int y, List<(int x, int y)> path)> queue = new();
            queue.Enqueue((start.x, start.y, new List<(int x, int y)> { start }));
            visited[start.x, start.y] = true;

            while (queue.Count > 0)
            {
                var (currentX, currentY, path) = queue.Dequeue();

                if ((currentX, currentY) == end)
                {
                    MarkPath(grid, path, start, end);
                    WriteOutput(grid, outputFile, true);
                    return true;
                }

                for (int i = 0; i < directions.GetLength(0); i++)
                {
                    int newX = currentX + directions[i, 0];
                    int newY = currentY + directions[i, 1];

                    if (IsValidMove(newX, newY, n, visited, grid))
                    {
                        visited[newX, newY] = true;
                        var newPath = new List<(int x, int y)>(path) { (newX, newY) };
                        queue.Enqueue((newX, newY, newPath));
                    }
                }
            }

            return false;
        }

        private static bool IsValidMove(int x, int y, int n, bool[,] visited, char[,] grid)
        {
            return x >= 0 && x < n && y >= 0 && y < n && !visited[x, y] && grid[x, y] != 'O';
        }

        private static void MarkPath(char[,] grid, List<(int x, int y)> path, (int x, int y) start, (int x, int y) end)
        {
            foreach (var (x, y) in path)
            {
                if (grid[x, y] == '.')
                {
                    grid[x, y] = '+';
                }
            }
            grid[start.x, start.y] = '@';
            grid[end.x, end.y] = 'X';
        }

        private static void WriteOutput(char[,] grid, string outputFile, bool pathFound)
        {
            File.WriteAllText(outputFile, pathFound ? "Y\n" : "N");
            int n = grid.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    File.AppendAllText(outputFile, grid[i, j].ToString());
                }
                File.AppendAllText(outputFile, "\n");
            }
        }
    }
}
