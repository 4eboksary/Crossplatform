using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Зчитування вхідних даних
        string[] input = File.ReadAllLines("C:\\Users\\pushkaruk\\Documents\\GitHub\\Crossplatform\\Lab2\\INPUT.TXT");
        string[] firstLine = input[0].Split(' ');
        int n = int.Parse(firstLine[0]); // Довжина рядка
        int k = int.Parse(firstLine[1]); // Не використовується в завданні
        int l = int.Parse(firstLine[2]); // Кількість запитів
        int[] queries = new int[l];
        for (int i = 0; i < l; i++)  // Fix: Loop from 0 to l - 1
        {
            queries[i] = int.Parse(input[i + 1]); // Fix: Correctly indexing from the second line
        }

        // Ініціалізація стеку
        Stack<int> stack = new Stack<int>();

        // Обробка запитів
        int[] results = new int[l];
        int currentDeletion = 1;
        for (int i = 0; i < l; i++)
        {
            int query = queries[i];
            while (stack.Count > 0 && stack.Peek() > query)
            {
                stack.Pop();
                currentDeletion++;
            }
            if (stack.Count > 0 && stack.Peek() == query)
            {
                stack.Pop();
                results[i] = currentDeletion;
            }
            else
            {
                results[i] = 0;
            }
            stack.Push(query);
        }

        // Запис результатів у файл
        using (StreamWriter writer = new StreamWriter("C:\\Users\\pushkaruk\\Documents\\GitHub\\Crossplatform\\Lab2\\OUTPUT.TXT"))
        {
            foreach (int result in results)
            {
                writer.WriteLine(result);
            }
        }
    }
}
