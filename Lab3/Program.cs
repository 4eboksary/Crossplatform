using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

class Pos
{
    public int i;
    public int j;

    public Pos(int i, int j)
    {
        this.i = i;
        this.j = j;
    }
}

class Program
{
    static void Main()
    {
        const int UNDEF = -1;
        string[] input = File.ReadAllLines("C:\\Users\\pushkaruk\\Documents\\GitHub\\Crossplatform\\Lab3\\INPUT.TXT");
        int n = int.Parse(input[0]);

        char[,] a = new char[n + 2, n + 2];
        int[,] len = new int[n + 2, n + 2];
        Queue<Pos> q = new Queue<Pos>();
        int dstI = UNDEF, dstJ = UNDEF;
        Pos startPos = null;

        // Initialize the grid and find start and destination points
        for (int i = 1; i <= n; i++)
        {
            string line = input[i];
            for (int j = 1; j <= n; j++)
            {
                a[i, j] = line[j - 1];

                if (a[i, j] == 'X')
                {
                    dstI = i;
                    dstJ = j;
                }
                else if (a[i, j] == '@')
                {
                    q.Enqueue(new Pos(i, j));
                    len[i, j] = 0;
                    startPos = new Pos(i, j);
                }
                else
                {
                    len[i, j] = UNDEF;
                }
            }
        }

        // Check if start or destination point is undefined
        if (startPos == null || dstI == UNDEF || dstJ == UNDEF)
        {
            WriteOutput("N");
            return;
        }

        // BFS to find the shortest path
        bool pathFound = false;
        while (q.Count > 0)
        {
            Pos cur = q.Dequeue();

            for (int di = -1; di <= 1; di++)
            {
                for (int dj = -1; dj <= 1; dj++)
                {
                    // Move in four directions: up, down, left, right
                    if (Math.Abs(di) + Math.Abs(dj) == 1)
                    {
                        int ni = cur.i + di;
                        int nj = cur.j + dj;

                        // Check if within bounds and valid path
                        if (ni > 0 && ni <= n && nj > 0 && nj <= n && a[ni, nj] != 'O' && len[ni, nj] == UNDEF)
                        {
                            len[ni, nj] = len[cur.i, cur.j] + 1;
                            q.Enqueue(new Pos(ni, nj));

                            if (ni == dstI && nj == dstJ)
                            {
                                pathFound = true;
                                break;
                            }
                        }
                    }
                }
                if (pathFound) break;
            }
            if (pathFound) break;
        }

        // Write the output
        if (!pathFound)
        {
            WriteOutput("N");
        }
        else
        {
            // Mark the path with '+'
            WriteOutput("Y", a, startPos, dstI, dstJ, len);
        }
    }

    static void WriteOutput(string outputFilePath, string result, char[,] a = null, Pos startPos = null, int dstI = 0, int dstJ = 0, int[,] len = null)
    {
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            if (result == "N")
            {
                writer.WriteLine("N");
                return;
            }

            writer.WriteLine("Y");

            // Відновлення шляху
            Pos curPos = new Pos(dstI, dstJ);
            while (curPos.i != startPos.i || curPos.j != startPos.j)
            {
                a[curPos.i, curPos.j] = '+';

                for (int di = -1; di <= 1; di++)
                {
                    for (int dj = -1; dj <= 1; dj++)
                    {
                        if (Math.Abs(di) + Math.Abs(dj) == 1)
                        {
                            int ni = curPos.i + di;
                            int nj = curPos.j + dj;

                            if (ni > 0 && ni < len.GetLength(0) && nj > 0 && nj < len.GetLength(1) && len[ni, nj] == len[curPos.i, curPos.j] - 1)
                            {
                                curPos = new Pos(ni, nj);
                                break;
                            }
                        }
                    }
                }
            }

            // Позначення стартової і кінцевої точок
            a[startPos.i, startPos.j] = '+';
            a[dstI, dstJ] = 'X';

            // Виведення оновленої сітки без зайвих пробілів та рядків
            for (int i = 1; i <= len.GetLength(0) - 2; i++)
            {
                for (int j = 1; j <= len.GetLength(1) - 2; j++)
                {
                    writer.Write(a[i, j]);
                }
                if (i != len.GetLength(0) - 2) // Не додаємо новий рядок після останнього рядка
                    writer.WriteLine();
            }
        }
    }
}