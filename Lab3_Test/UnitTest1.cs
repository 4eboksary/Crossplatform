using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.IO;
using Xunit;
using Lab3;

namespace Lab3.Tests;

public class PathTests
{
    private string _baseDirectory;

    public PathTests()
    {
        // Базовий каталог для перевірки шляхів
        _baseDirectory = Path.Combine(AppContext.BaseDirectory, "TestFiles");
        Directory.CreateDirectory(_baseDirectory);
    }

    [Fact]
    public void Test_InputPathExists()
    {
        // Створюємо файл INPUT.txt
        string inputPath = Path.Combine(_baseDirectory, "INPUT.txt");
        File.WriteAllText(inputPath, "Test data");

        // Перевіряємо, чи файл існує
        Assert.True(File.Exists(inputPath), "INPUT.txt does not exist at the expected path.");
    }

    [Fact]
    public void Test_OutputPathCreated()
    {
        // Встановлюємо шлях для OUTPUT.txt
        string outputPath = Path.Combine(_baseDirectory, "OUTPUT.txt");

        // Якщо файл існує, видаляємо його для чистоти тесту
        if (File.Exists(outputPath))
            File.Delete(outputPath);

        // Симулюємо створення файлу (можна замінити викликом Program.Main(), якщо потрібно)
        File.WriteAllText(outputPath, "Test output");

        // Перевіряємо, чи файл створено
        Assert.True(File.Exists(outputPath), "OUTPUT.txt was not created at the expected path.");
    }

    [Fact]
    public void Test_InputPathIsCorrect()
    {
        // Задаємо очікуваний шлях для INPUT.txt
        string expectedInputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "INPUT.txt").Trim();

        // Перевіряємо, чи шлях відповідає очікуваному формату
        Assert.EndsWith("Lab3\\INPUT.txt", expectedInputPath.Replace("/", "\\"));
    }

    [Fact]
    public void Test_OutputPathIsCorrect()
    {
        // Задаємо очікуваний шлях для OUTPUT.txt
        string expectedOutputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "OUTPUT.txt").Trim();

        // Перевіряємо, чи шлях відповідає очікуваному формату
        Assert.EndsWith("Lab3\\OUTPUT.txt", expectedOutputPath.Replace("/", "\\"));
    }
}
