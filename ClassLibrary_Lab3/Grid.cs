namespace ClassLibrary_Lab3
{
    public class Grid
    {
        public int Size { get; private set; }
        public char[,] Cells { get; private set; }
        public (int x, int y) Start { get; private set; }
        public (int x, int y) End { get; private set; }

        public Grid(string inputPath)
        {
            var input = File.ReadAllLines(inputPath);

            if (input.Length == 0)
                throw new Exception("Вхідний файл порожній.");

            if (!int.TryParse(input[0], out int n))
                throw new Exception("Перший рядок не є дійсним числом.");

            Size = n;
            Cells = new char[n, n];
            Start = End = (-1, -1);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Cells[i, j] = input[i + 1][j];
                    if (Cells[i, j] == '@') Start = (i, j);
                    if (Cells[i, j] == 'X') End = (i, j);
                }
            }

            if (Start == (-1, -1) || End == (-1, -1))
                throw new Exception("Не знайдено стартову або кінцеву точку.");
        }

        public void SaveResult(string outputPath, bool pathFound)
        {
            using (var writer = new StreamWriter(outputPath))
            {
                writer.WriteLine(pathFound ? "Y" : "N");
                if (pathFound)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        for (int j = 0; j < Size; j++)
                            writer.Write(Cells[i, j]);
                        writer.WriteLine();
                    }
                }
            }
        }
    }
}
