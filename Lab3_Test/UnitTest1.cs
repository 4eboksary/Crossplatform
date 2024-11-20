using System;
using Xunit;
using System.IO;
using System.Collections.Generic;

namespace UnitTests
{
    public class ProgramTests
    {
        // Test case to check if array 'a' and 'len' are initialized properly
        [Fact]
        public void TestArrayInitialization()
        {
            string[] input = new string[]
            {
                "5",
                ".....",
                "..@..",
                "..O..",
                "..X..",
                "....."
            };

            int n = int.Parse(input[0]);
            char[,] a = new char[n + 2, n + 2];
            int[,] len = new int[n + 2, n + 2];

            int dstI = -1, dstJ = -1;
            Pos startPos = null;

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
                        startPos = new Pos(i, j);
                    }
                    else
                    {
                        len[i, j] = -1;
                    }
                }
            }

            Assert.Equal('@', a[2, 3]);
            Assert.Equal('X', a[4, 3]);
            Assert.Equal(-1, len[2, 3]);
        }

        // Test case to check pathfinding logic
        [Fact]
        public void TestPathfinding()
        {
            string[] input = new string[]
            {
                "5",
                ".....",
                "..@..",
                "..O..",
                "..X..",
                "....."
            };

            int n = int.Parse(input[0]);
            char[,] a = new char[n + 2, n + 2];
            int[,] len = new int[n + 2, n + 2];
            Queue<Pos> q = new Queue<Pos>();
            int dstI = -1, dstJ = -1;
            Pos startPos = null;

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
                        len[i, j] = -1;
                    }
                }
            }

            bool pathFound = false;
            while (q.Count > 0)
            {
                Pos cur = q.Dequeue();
                for (int di = -1; di <= 1; di++)
                {
                    for (int dj = -1; dj <= 1; dj++)
                    {
                        if (Math.Abs(di) + Math.Abs(dj) == 1)
                        {
                            int ni = cur.i + di;
                            int nj = cur.j + dj;

                            if (ni > 0 && ni <= n && nj > 0 && nj <= n && a[ni, nj] != 'O' && len[ni, nj] == -1)
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

            Assert.True(pathFound); // Path should be found in this case.
        }

        // Test case to check the WriteOutput function
        [Fact]
        public void TestWriteOutput()
        {
            string[] input = new string[]
            {
                "5",
                ".....",
                "..@..",
                "..O..",
                "..X..",
                "....."
            };

            string outputPath = Path.GetTempFileName();
            try
            {
                int n = int.Parse(input[0]);
                char[,] a = new char[n + 2, n + 2];
                int[,] len = new int[n + 2, n + 2];
                Queue<Pos> q = new Queue<Pos>();
                int dstI = -1, dstJ = -1;
                Pos startPos = null;

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
                            len[i, j] = -1;
                        }
                    }
                }

                bool pathFound = false;
                while (q.Count > 0)
                {
                    Pos cur = q.Dequeue();
                    for (int di = -1; di <= 1; di++)
                    {
                        for (int dj = -1; dj <= 1; dj++)
                        {
                            if (Math.Abs(di) + Math.Abs(dj) == 1)
                            {
                                int ni = cur.i + di;
                                int nj = cur.j + dj;

                                if (ni > 0 && ni <= n && nj > 0 && nj <= n && a[ni, nj] != 'O' && len[ni, nj] == -1)
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

                if (pathFound)
                {
                    WriteOutput(outputPath, "Y", a, startPos, dstI, dstJ, len);
                }
                else
                {
                    WriteOutput(outputPath, "N");
                }

                var output = File.ReadAllLines(outputPath);
                Assert.Contains("Y", output[0]); // Check that the output file contains "Y"
            }
            finally
            {
                File.Delete(outputPath);
            }
        }

        private void WriteOutput(string outputFilePath, string result, char[,] a = null, Pos startPos = null, int dstI = 0, int dstJ = 0, int[,] len = null)
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                if (result == "N")
                {
                    writer.WriteLine("N");
                    return;
                }
                writer.WriteLine("Y");
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

                a[startPos.i, startPos.j] = '+';
                a[dstI, dstJ] = 'X';

                for (int i = 1; i <= len.GetLength(0) - 2; i++)
                {
                    for (int j = 1; j <= len.GetLength(1) - 2; j++)
                    {
                        writer.Write(a[i, j]);
                    }
                    if (i != len.GetLength(0) - 2)
                        writer.WriteLine();
                }
            }
        }
    }
}
