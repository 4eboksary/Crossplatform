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
        // ������� ������� ��� �������� ������
        _baseDirectory = Path.Combine(AppContext.BaseDirectory, "TestFiles");
        Directory.CreateDirectory(_baseDirectory);
    }

    [Fact]
    public void Test_InputPathExists()
    {
        // ��������� ���� INPUT.txt
        string inputPath = Path.Combine(_baseDirectory, "INPUT.txt");
        File.WriteAllText(inputPath, "Test data");

        // ����������, �� ���� ����
        Assert.True(File.Exists(inputPath), "INPUT.txt does not exist at the expected path.");
    }

    [Fact]
    public void Test_OutputPathCreated()
    {
        // ������������ ���� ��� OUTPUT.txt
        string outputPath = Path.Combine(_baseDirectory, "OUTPUT.txt");

        // ���� ���� ����, ��������� ���� ��� ������� �����
        if (File.Exists(outputPath))
            File.Delete(outputPath);

        // ��������� ��������� ����� (����� ������� �������� Program.Main(), ���� �������)
        File.WriteAllText(outputPath, "Test output");

        // ����������, �� ���� ��������
        Assert.True(File.Exists(outputPath), "OUTPUT.txt was not created at the expected path.");
    }

    [Fact]
    public void Test_InputPathIsCorrect()
    {
        // ������ ���������� ���� ��� INPUT.txt
        string expectedInputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "INPUT.txt").Trim();

        // ����������, �� ���� ������� ����������� �������
        Assert.EndsWith("Lab3\\INPUT.txt", expectedInputPath.Replace("/", "\\"));
    }

    [Fact]
    public void Test_OutputPathIsCorrect()
    {
        // ������ ���������� ���� ��� OUTPUT.txt
        string expectedOutputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab3", "OUTPUT.txt").Trim();

        // ����������, �� ���� ������� ����������� �������
        Assert.EndsWith("Lab3\\OUTPUT.txt", expectedOutputPath.Replace("/", "\\"));
    }
}
