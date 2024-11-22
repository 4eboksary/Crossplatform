using System;
using System.Collections.Generic;
using System.IO;
using ClassLibrary_Lab3;

class Program
{
    static void Main()
    {
        try
        {
            string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "INPUT.txt").Trim();
            string outputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "OUTPUT.txt").Trim();

            var grid = new Grid(inputPath);
            var pathFinder = new PathFinder(grid);
            bool pathFound = pathFinder.FindPath();
            grid.SaveResult(outputPath, pathFound);

            Console.WriteLine(pathFound ? "Шлях знайдено!" : "Шлях не знайдено.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Сталася помилка: {ex.Message}");
        }
    }
}
