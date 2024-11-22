using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary_Lab4
{
    public class Lab1
    {
        public static void GoLab1(string inputFile, string outputFile)
        {
            try
            {
                // Read input data
                string input = ReadInputData(inputFile);

                // Validate input string
                if (!IsStringOnlyLetters(input))
                {
                    throw new ArgumentException("Input string must contain only letters.");
                }

                // Generate all permutations
                StringBuilder permutationsResult = new StringBuilder();
                GeneratePermutations(input, 0, permutationsResult);

                // Save results to the output file
                File.WriteAllText(outputFile, permutationsResult.ToString().Trim());

                Console.WriteLine($"Results have been successfully saved to {outputFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static string ReadInputData(string inputFile)
        {
            if (!File.Exists(inputFile))
            {
                throw new FileNotFoundException($"Input file not found: {inputFile}");
            }

            string input = File.ReadAllText(inputFile).Trim();
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input string cannot be empty or null.");
            }
            return input;
        }

        public static bool IsStringOnlyLetters(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z]+$");
        }

        public static void GeneratePermutations(string text, int index, StringBuilder result)
        {
            if (index == text.Length - 1)
            {
                result.AppendLine(text);
                return;
            }

            for (int i = index; i < text.Length; i++)
            {
                // Swap characters
                text = Swap(text, index, i);

                // Recursively generate permutations
                GeneratePermutations(text, index + 1, result);

                // Swap back to restore the original string
                text = Swap(text, index, i);
            }
        }

        public static string Swap(string str, int i, int j)
        {
            if (i < 0 || j < 0 || i >= str.Length || j >= str.Length)
            {
                throw new IndexOutOfRangeException("Indexes must be within the bounds of the string.");
            }

            char[] charArray = str.ToCharArray();
            char temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;

            return new string(charArray);
        }
    }
}
